using System;
using UEAssistantMobile.Models;

namespace UEAssistantMobile
{
    public class Grade
    {
        public string Subject { get; set; }
        public string TypeOfSubject { get; set; }
        public string GradeValue { get; set; }
        public int AmountOfECTS { get; set; }
        public DateTime DateOfGrade { get; set; }
        public int NumberOfTerm { get; set; }
        public string NameOfTeacher { get; set; }
        public string TypeOfSubjectPass { get; set; }

        public string FieldOfStudy { get; set; }
        public string EditionOfFieldOfStudy { get; set; }
        public string SemesterOfEdition { get; set; }

        public Grade(string subject, string typeOfSubject, string gradeValue, int amountOfECTS,
                     DateTime dateOfGrade, int numberOfTerm, string nameOfTeacher, string typeOfSubjectPass)
        {
            this.Subject = subject;
            this.TypeOfSubject = typeOfSubject;
            this.GradeValue = gradeValue;
            this.AmountOfECTS = amountOfECTS;
            this.DateOfGrade = dateOfGrade;
            this.NumberOfTerm = numberOfTerm;
            this.NameOfTeacher = nameOfTeacher;
            this.TypeOfSubjectPass = typeOfSubjectPass;
        }

        public Grade(string fieldOfStudy, string editionOfFieldOfStudy, string semesterOfEdition, string subject, string typeOfSubject, string gradeValue,
                     int amountOfECTS, DateTime dateOfGrade, int numberOfTerm, string nameOfTeacher, string typeOfSubjectPass)
        {
            this.FieldOfStudy = fieldOfStudy;
            this.EditionOfFieldOfStudy = editionOfFieldOfStudy;
            this.SemesterOfEdition = semesterOfEdition;
            this.Subject = subject;
            this.TypeOfSubject = typeOfSubject;
            this.GradeValue = gradeValue;
            this.AmountOfECTS = amountOfECTS;
            this.DateOfGrade = dateOfGrade;
            this.NumberOfTerm = numberOfTerm;
            this.NameOfTeacher = nameOfTeacher;
            this.TypeOfSubjectPass = typeOfSubjectPass;
        }

        public GradeModel ConvertToGradeModel()
        {
            return new GradeModel
            {
                FieldOfStudy = this.FieldOfStudy,
                Edition = this.EditionOfFieldOfStudy,
                Semester = this.SemesterOfEdition,
                Subject = this.Subject,
                TypeOfSubject = this.TypeOfSubject,
                GradeValue = this.GradeValue,
                Ects = this.AmountOfECTS.ToString(),
                DateOfGrade = this.DateOfGrade.ToShortDateString(),
                NumberOfTerm= this.NumberOfTerm.ToString(),
                Teacher = this.NameOfTeacher,
                TypeOfPass = this.TypeOfSubjectPass

            };
        }

        new public string ToString()
        {
            return string.Format(
            "Kierunek: {0}" +
            "\nEdycja: {1}" +
            "\nSemestr: {2}" +
            "\nPrzedmiot: {3}" +
            "\nTyp: {4}" +
            "\nOcena: {5}" +
            "\nPunkty ECTS: {6}" +
            "\nData wystawienia: {7}" +
            "\nNumer terminu: {8}" +
            "\nNauczyciel: {9}" +
            "\nTyp zaliczenia: {10}",
            FieldOfStudy, EditionOfFieldOfStudy, SemesterOfEdition, Subject, TypeOfSubject,
            GradeValue, AmountOfECTS, DateOfGrade.ToShortDateString(), NumberOfTerm, NameOfTeacher, TypeOfSubjectPass);
        }
    }
}
