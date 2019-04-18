using System;
using System.ComponentModel;

namespace UEAssistantMobile.Models
{
    public class GradeModel : INotifyPropertyChanged
    {
        string fieldOfStudy;
        public string FieldOfStudy
        {
            get { return fieldOfStudy; }
            set
            {
                fieldOfStudy = value;
                OnPropertyChanged("FieldOfStudy");
            }
        }
        string edition;
        public string Edition
        {
            get { return edition; }
            set
            {
                edition = value;
                OnPropertyChanged("Edition");
            }
        }
        string semester;
        public string Semester
        {
            get { return semester; }
            set
            {
                semester = value;
                OnPropertyChanged("Semester");
            }
        }
        string subject;
        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }
        string typeOfSubject;
        public string TypeOfSubject
        {
            get { return typeOfSubject; }
            set
            {
                typeOfSubject = value;
                OnPropertyChanged("TypeOfSubject");
            }
        }

        string gradeValue;
        public string GradeValue
        {
            get { return gradeValue; }
            set
            {
                gradeValue = value;
                OnPropertyChanged("GradeValue");
            }
        }

        string ects;
        public string Ects
        {
            get { return ects; }
            set
            {
                ects = value;
                OnPropertyChanged("Ects");
            }
        }

        string dateOfGrade;
        public string DateOfGrade
        {
            get { return dateOfGrade; }
            set
            {
                dateOfGrade = value;
                OnPropertyChanged("DateOfGrade");
            }
        }

        string numberOfTerm;
        public string NumberOfTerm
        {
            get { return numberOfTerm; }
            set
            {
                numberOfTerm = value;
                OnPropertyChanged("NumberOfTerm");
            }
        }

        string teacher;
        public string Teacher
        {
            get { return teacher; }
            set
            {
                teacher = value;
                OnPropertyChanged("Teacher");
            }
        }

        string typeOfPass;
        public string TypeOfPass
        {
            get { return typeOfPass; }
            set
            {
                typeOfPass = value;
                OnPropertyChanged("TypeOfPass");
            }
        }

        bool visibility;
        public bool Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
