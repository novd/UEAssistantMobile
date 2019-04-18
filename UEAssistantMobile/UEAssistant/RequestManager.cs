using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UEAssistantMobile
{
    public class RequestManager
    {
        private string Login { get; set; }
        private string Password { get; set; }
        private string StudentId { get; set; }
        private HttpClient Client { get; set; }
        private CookieContainer CookieContainer { get; set; }
        private HttpClientHandler Handler { get; set; }

        private Uri baseAddress;
        private string signInAddress;
        private string gradesAddress;

        private JArray fieldsOfStudy;
        private JArray editionsOfFieldsOfStudy;

        public RequestManager(string login, string password)
        {
            this.Login = login;
            this.Password = password;

            this.baseAddress = new Uri("https://e-uczelnia.ue.katowice.pl/");
            this.signInAddress = "cas/login?service=https%3A%2F%2Fe-uczelnia.ue.katowice.pl%2Fwu%2Fj_spring_cas_security_check";
            this.gradesAddress = "wu/redirectToUrl?redirectUrlParameter=pages/studia/oceny.html";
        }

        public bool SignIn()
        {
            this.CookieContainer = new CookieContainer();
            this.Handler = new HttpClientHandler() { CookieContainer = CookieContainer };
            this.Client = new HttpClient(Handler) { BaseAddress = baseAddress };

            Client.GetAsync("/").Result.EnsureSuccessStatusCode();

            var userData = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("username", Login),
                new KeyValuePair<string, string>("password", Password)
             };
            var headers = HtmlScrapper.GetSingInPostHeaders(Client.GetAsync(signInAddress).Result.Content.ReadAsStringAsync().Result);
            userData = userData.Concat(headers).ToList();

            var content = new FormUrlEncodedContent(userData);

            try
            {
                Client.PostAsync(signInAddress, content).Result.EnsureSuccessStatusCode();
                Client.GetAsync("/wu/start?&locale=pl").Result.EnsureSuccessStatusCode();
                StudentId = this.GetStudentId();
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine("Sing in failed: " + e.Message);
                return false;
            }


        }

        public async Task<List<Grade>> GetGradesAsync(List<GradesRequestConfiguration> ListOfConfigurations) // null return list of all grades
        {
            //Getting fields of study to collect essential data
            var fieldsOfStudyAddress = "/wsrest/rest/templating/student/kierunki";
            var fieldOfStudyjsonAsString = string.Format("{{\"id_studenta\":{0}}}", StudentId);

            fieldsOfStudy = (JArray)this.PostJson(fieldsOfStudyAddress, fieldOfStudyjsonAsString, true);
            var fieldOfStudyIds = fieldsOfStudy.Select(element => element["id_kierunek_student"].ToString());
            //==================================================================================================

            //Getting editions of study to collect essential data
            editionsOfFieldsOfStudy = new JArray();
            var editionsOfStudyIds = new Dictionary<string, List<string>>();
            foreach (var fieldId in fieldOfStudyIds)
            {
                var editionsOfStudyAddress = "/wsrest/rest/templating/student/edycje";
                var editionsJsonAsString = string.Format("{{\"id_studenta\":{0}, \"id_kierunek_student\":{1}}}",
                                                 StudentId, fieldId);

                var jsonArrayResponse = (JArray)this.PostJson(editionsOfStudyAddress, editionsJsonAsString, false);
                editionsOfFieldsOfStudy.Merge(jsonArrayResponse);
                editionsOfStudyIds.Add(fieldId, jsonArrayResponse
                                                            .Select(element => element["id_edycja"].ToString()).ToList());
            }
            //==================================================================================================

            //Getting semesters of given edition to collect essential data
            var semestersOfFieldOfStudy = new List<Tuple<string, string, string>>();
            foreach (KeyValuePair<string, List<string>> edition in editionsOfStudyIds)
            {
                foreach (var editionId in edition.Value)
                {
                    var semestersOfEditionAddress = "/wsrest/rest/templating/student/semestry";
                    var semestersJsonAsString = string.Format("{{\"id_studenta\":{0}, \"id_kierunek_student\":{1}, \"id_edycja\":{2}}}",
                                                         StudentId, edition.Key, editionId);

                    //semestersOfEdition.Add(editionId, ((JArray)this.PostJson(semestersOfEditionAddress, semestersJsonAsString, false))
                    //                                                    .Select(element => element["semestr"].ToString()).ToList());
                    var arrayOfSemestres = ((JArray)this.PostJson(semestersOfEditionAddress, semestersJsonAsString, false))
                                                                        .Select(element => element["semestr"].ToString()).ToList();
                    foreach (var semester in arrayOfSemestres)
                    {
                        semestersOfFieldOfStudy.Add(Tuple.Create(edition.Key, editionId, semester));
                    }
                }
            }

            var grades = new List<Grade>();
            if (ListOfConfigurations != null)
            {
                var selectedSemesters = new List<Tuple<string, string, string>>();
                foreach (var configuration in ListOfConfigurations)
                {
                    selectedSemesters.AddRange(semestersOfFieldOfStudy
                                              .Where(semester => (semester.Item1 == configuration.FieldOfStudyId)
                                                          && (semester.Item2 == configuration.EditionOfFieldId)
                                                          && (semester.Item3 == configuration.SemesterOfEdition))
                                                          .ToList());
                }

                return await this.RenderGradesForSemester(selectedSemesters);
            }

            //if ListOfConfigurations is null, then return all known grades
            return await this.RenderGradesForSemester(semestersOfFieldOfStudy);

        }

        private JToken PostJson(string address, string jsonAsString, bool needToAuthenticate)
        {
            if (needToAuthenticate)
                this.Authenticate().EnsureSuccessStatusCode();

            var content = new StringContent(jsonAsString, Encoding.UTF8, "application/json");
            var jsonResponse = JObject.Parse(Client.PostAsync(address, content).Result.Content.ReadAsStringAsync().Result);

            return jsonResponse["result"];
        }

        private async Task<List<Grade>> RenderGradesForSemester(List<Tuple<string, string, string>> semesters)
        {
            var gradesToReturn = new List<Grade>();
            var tasks = new List<Task>();
            foreach (var semester in semesters)
            {
                var renderGradesAddress = "/wsrest/rest/templating/renderTemplate";
                var renderJsonAsString = string.Format("{{\"id_studenta\":{0}, \"id_kierunek_student\":{1}, \"id_edycja\":{2}," +
                                           " \"semestr\":{3}, \"klucz\":{4}, \"lang\":{5}}}",
                                           StudentId, semester.Item1, semester.Item2, semester.Item3, "\"student.oceny\"", "\"pl\"");

                tasks.Add(Task.Run(() =>
                {
                    var grades = HtmlScrapper.GetGradesFromGradesPage(
                          this.PostJson(renderGradesAddress, renderJsonAsString, true).ToString());
                    foreach (var grade in grades)
                    {
                        grade.FieldOfStudy = fieldsOfStudy.
                                                First(element => element["id_kierunek_student"].ToString() == semester.Item1)["nazwa"]
                                                .ToString();
                        grade.EditionOfFieldOfStudy = editionsOfFieldsOfStudy
                                                        .First(element =>
                                                        element["id_kierunek_student"].ToString() == semester.Item1)["nazwa_edycja"]
                                                        .ToString(); 


                        grade.SemesterOfEdition = semester.Item3;
                    }


                    gradesToReturn.AddRange(grades);
                }));

            }
            await Task.WhenAll(tasks.ToArray());
            return gradesToReturn;
        }

        private string GetStudentId()
        {
            var gradesPageHtml = Client.GetAsync(gradesAddress).Result.Content.ReadAsStringAsync().Result;
            return HtmlScrapper.GetStudentIdFromGradesPage(gradesPageHtml);
        }

        private HttpResponseMessage Authenticate()
        {
            var authenticateAddress = "/wsrest/rest/authenticate";
            return Client.GetAsync(authenticateAddress).Result;
        }


    }
}
