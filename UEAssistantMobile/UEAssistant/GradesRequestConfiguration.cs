using System;
namespace UEAssistantMobile
{
    public class GradesRequestConfiguration
    {
        public string FieldOfStudyId { get; private set; }
        public string EditionOfFieldId { get; private set; }
        public string SemesterOfEdition { get; private set; }

        public GradesRequestConfiguration(string fieldOfStudyId, string editionOfFieldId, string semesterOfEditionId)
        {
            this.FieldOfStudyId = fieldOfStudyId;
            this.EditionOfFieldId = editionOfFieldId;
            this.SemesterOfEdition = semesterOfEditionId;
        }
    }
}
