using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UEAssistantMobile.Models
{
    public class SemesterModel : INotifyPropertyChanged
    {
        string numberOfSemester;
        public string NumberOfSemester
        {
            get { return numberOfSemester; }
            set
            {
                numberOfSemester = value;
                OnPropertyChanged("NumberOfSemester");
            }
        }

        List<GradeModel> grades;
        public List<GradeModel> Grades
        {
            get { return grades; }
            set
            {
                grades = value;
                OnPropertyChanged("Grades");
            }
        }
        bool visibility;
        public bool Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                IconSource = visibility ? "arrow_up.png" : "arrow_down.png";
                OnPropertyChanged("Visibility");
            }
        }

        string iconSource = "arrow_down.png";
        public string IconSource
        {
            get { return iconSource; }
            set
            {
                iconSource = value;
                OnPropertyChanged("IconSource");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
