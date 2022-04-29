using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spanzuratoarea_MVP.Model
{
    class Letter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        static public event PropertyChangedEventHandler StaticPropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        static private void StaticNotifyPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        char letterName;
        bool isHide = false;

        public char LetterName
        {
            get
            {
                if (!isHide)
                    return letterName;
                else return '_';
            }
            set
            {
                if (value != letterName)
                {
                    letterName = value;
                    NotifyPropertyChanged("LetterName");
                }
            }
        }

        public char RealLetter
        {
            get
            {
                return letterName;
            }
        }

        public bool IsHide
        {
            get => isHide; set
            {
                if (value != isHide)
                {
                    isHide = value;
                    NotifyPropertyChanged("IsHide");
                    NotifyPropertyChanged("LetterName");
                }
            }
        }

        public Letter(char letterName, bool isHide)
        {
            this.letterName = letterName;
            this.isHide = isHide;
        }
        


    }
}
