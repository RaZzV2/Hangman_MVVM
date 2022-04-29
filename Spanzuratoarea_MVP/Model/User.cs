using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spanzuratoarea_MVP.Model
{
    class User : INotifyPropertyChanged
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

        private string name;
        private string image;
        private int score = 0;
        private int level = 1;
        private int loses = 0;
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public int Score
        {
            get => score;
            set
            {
                if(value != score)
                {
                    score = value;
                    NotifyPropertyChanged("Score");
                }
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                if (value != level)
                {
                    level = value;
                    NotifyPropertyChanged("Level");
                }
            }
        }
        public int Loses
        {
            get
            {
                return loses;
            }
            set
            {
                if (value != loses)
                {
                    loses = value;
                    NotifyPropertyChanged("Loses");
                }
            }
        }

        public User(string name)
        {
            this.name = name;
        }

        public User(string name, string image)
        {
            this.name = name;
            this.image = image;
        }

        public User(string name, string image, int score)
        {
            this.name = name;
            this.image = image;
            this.score = score;
        }

        public User(string name, string image, int score, int level)
        {
            this.name = name;
            this.image = image;
            this.score = score;
            this.level = level;
        }

        public User(string name, string image, int score, int level, int loses)
        {
            this.name = name;
            this.image = image;
            this.score = score;
            this.level = level;
            this.loses = loses;
        }

    }
}
