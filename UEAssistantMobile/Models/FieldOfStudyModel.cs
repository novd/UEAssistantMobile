using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UEAssistantMobile.Models
{
    public class FieldOfStudyModel : INotifyPropertyChanged
    {
        string nameOfField;
        public string NameOfField
        {
            get { return nameOfField; }
            set
            {
                nameOfField = value;
                OnPropertyChanged("NameOfField");
            }
        }

        List<EditionModel> editions;
        public List<EditionModel> Editions
        {
            get { return editions; }
            set
            {
                editions = value;
                OnPropertyChanged("Editions");
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

        string iconSource= "arrow_down.png";
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
