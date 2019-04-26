using System;
using System.ComponentModel;
using System.Drawing;

namespace UEAssistantMobile.ViewModels
{
    public class IndirectViewModel : INotifyPropertyChanged
    {
        float opacity = 1;
        public float OpacityEffect
        {
            get { return opacity; }
            set
            {
                opacity = value;
                OnPropertyChanged("OpacityEffect");
            }
        }


        float rotation;
        public float RotationEffect 
        {
            get { return rotation; }
            set
            {
                rotation = value;
                OnPropertyChanged("RotationEffect");
            }
        }

        string infoText;
        public string InfoText
        {
            get { return infoText; }
            set
            {
                infoText = value;
                OnPropertyChanged("InfoText");
            }
        }

        #region PropertyChanded
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
