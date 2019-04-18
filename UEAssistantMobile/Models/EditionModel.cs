using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UEAssistantMobile.Models
{
    public class EditionModel : INotifyPropertyChanged
    {
        string nameOfEdition;
        public string NameOfEdition
        {
            get { return nameOfEdition; }
            set
            {
                nameOfEdition = value;
                OnPropertyChanged("NameOfEdition");
            }
        }

        List<SemesterModel> semesters;
        public List<SemesterModel> Semesters
        {
            get { return semesters; }
            set
            {
                semesters = value;
                OnPropertyChanged("Semesters");
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
