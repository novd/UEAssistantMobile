using System;
using System.ComponentModel;
using System.Drawing;

namespace UEAssistantMobile
{
    public class LoginViewModel : IEffectable,INotifyPropertyChanged
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
        public float RotationEffect { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorEffect { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #region PropertyChanded
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
