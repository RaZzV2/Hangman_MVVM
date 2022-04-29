using Spanzuratoarea_MVP.Model;
using Spanzuratoarea_MVP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Spanzuratoarea_MVP.ViewModel
{
    class UserModel : INotifyPropertyChanged
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
        private string usernameContent = "";
        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();

        static public List<string> Images { get; } = new List<string>();

        static public int indexCurrent = 0;

        private string currentLocation = "";

        public UserModel()
        {
            Images.Add(@"D:\Projects\C#\Spanzuratoarea_MVP\images\1.jpg");
            Images.Add(@"D:\Projects\C#\Spanzuratoarea_MVP\images\2.png");
            Images.Add(@"D:\Projects\C#\Spanzuratoarea_MVP\images\3.png");
            Images.Add(@"D:\Projects\C#\Spanzuratoarea_MVP\images\4.png");
            currentLocation = Images[0];
            Users.Add(new User("Razz", @"D:\Projects\C#\Spanzuratoarea_MVP\images\3.png", 0,0,0));
            SignUp = new RelayCommand(signUp);
            NewUser = new RelayCommand(newUser);
            LeftButton = new RelayCommand(leftButton);
            RightButton = new RelayCommand(rightButton);
            Cancel = new RelayCommand(cancel);
            RemoveUser = new RelayCommand(removeUser);
            PlayButton = new RelayCommand(playButton);
        }
        public ICommand PlayButton { get; }
        public ICommand LeftButton { get; }
        public ICommand Cancel { get; }

        public ICommand NewGame { get; }
        public ICommand RemoveUser { get; }

        public ICommand RightButton { get; }
        public ICommand NewUser { get; }

        void playButton(object e)
        {
           // PlayWindow playWindow = new PlayWindow();
            if(selectedIndex != -1)
            {
                var vm = new GameModel();
                var hangmanWindow = new PlayWindow
                {
                    DataContext = vm
                };
                vm.OnRequestClose += (s, e) => hangmanWindow.Close();
                hangmanWindow.ShowDialog();
            }
        }
        static void newUser(object e)
        {
            NewUserWindow newUserWindow = new NewUserWindow();
            newUserWindow.ShowDialog();
        }

        void removeUser(object e)
        {
            if(selectedIndex != -1)
            Users.RemoveAt(selectedIndex);
        }

        static void cancel(object e)
        {
            Environment.Exit(0);
        }
         

        public ICommand SignUp { get; }

        public string CurrentLocation
        {
            get => currentLocation;
            set
            {
                if(value != currentLocation)
                {
                    currentLocation = value;
                    NotifyPropertyChanged("CurrentLocation");
                }
            }
        }

        public static User currentUser = null;

        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if(value != selectedIndex)
                {
                    selectedIndex = value;
                    if (selectedIndex != -1)
                    {
                        currentUser = Users[selectedIndex];
                        NotifyPropertyChanged("SelectedIndex");
                    }
                }
            }
        }
        void leftButton(object e)
        {
            if (indexCurrent == 0)
            {
                indexCurrent = 3;
            }
            else
            {
                --indexCurrent;
            }
            CurrentLocation = Images[indexCurrent];
        }

        void rightButton(object e)
        {
            if(indexCurrent == 3)
            {
                indexCurrent = 0;
            }
            else
            {
                ++indexCurrent;
            }
            CurrentLocation = Images[indexCurrent];
        }

        public string UsernameContent
        {
            get => usernameContent;
            set
            {
                if (value != usernameContent)
                {
                    usernameContent = value;
                    NotifyPropertyChanged("UsernameContent");
                }
            }
        }

        void signUp(object e)
        {
            if (UsernameContent == "")
            {
                MessageBox.Show("Username can't be empty!");
            }
            else
            {
                Users.Add(new User(UsernameContent, currentLocation,0,1,0));
            }
        }

    }
}
