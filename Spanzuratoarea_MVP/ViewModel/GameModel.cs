using Spanzuratoarea_MVP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Spanzuratoarea_MVP.ViewModel
{
    class GameModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler OnRequestClose;

        static public event PropertyChangedEventHandler StaticPropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        static private void StaticNotifyPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }


        ObservableCollection<Letter> letterList = new ObservableCollection<Letter>();
        List<char> usedLetters = new List<char>();
        private double timerBar;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public double TimerBar { get => timerBar; set => SetProperty(ref timerBar, value); }
        public GameModel()
        {
            NewGameCommand = new RelayCommand(newGameCommand);
            PressKey = new RelayCommand(pressKey);
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            ResetGameCommand = new RelayCommand(resetGameCommand);
            StatisticsGameCommand = new RelayCommand(statisticsGameCommand);
            ExitGameCommand = new RelayCommand(exitGameCommand);
            SaveGameCommand = new RelayCommand(saveGameCommand);
        }

        public ICommand SaveGameCommand { get; }
        public ICommand ExitGameCommand { get; }

        void saveGameCommand(object e)
        {
            string userDirectory = "Saves/" + UserModel.currentUser.Name;
            if (!File.Exists(userDirectory))
                System.IO.Directory.CreateDirectory(userDirectory);
            DirectoryInfo saveFileDirInfo = new DirectoryInfo("./Saves/" + UserModel.currentUser.Name);
            int fileCount = saveFileDirInfo.GetFiles().Length;
            string saveName = "save" + fileCount;
            string filePath = "Saves/" + UserModel.currentUser.Name + "/" + saveName + ".txt";
            string usedLettersStr = "";
            if (usedLetters.Count != 0)
            {
                foreach (var usedLetter in usedLetters)
                    usedLettersStr = usedLettersStr + usedLetter + ' ';
                usedLettersStr = usedLettersStr.Remove(usedLettersStr.Length - 1, 1);
            }

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Your score is: " + UserModel.currentUser.Score);
                    sw.WriteLine("Remaining attempts: " + Attempts);
                    sw.WriteLine("Remaining time: " + (60 - TimerBar));
                    sw.WriteLine("Current word is: " + Word);
                    sw.WriteLine("Real word is: " + realWord);
                    sw.WriteLine("Used letters are: " + usedLettersStr);
                }
                MessageBox.Show("Saved successfully!");
            }
        }
        void exitGameCommand(object e)
        {
            OnRequestClose(this, new EventArgs());
        }

        private void resetGameCommand(object e)
        {
            attempts = 5;
            isFinished = false;
            timerBar = 0;
            dispatcherTimer.Start();
            letterList.Clear();
            NotifyPropertyChanged("Word");
            NotifyPropertyChanged("Attempts");
        }

        public string Word
        {
            get
            {
                string aux = "";
                foreach (Letter letter in letterList)
                {
                    aux += letter.LetterName;
                }
                return aux;
            }
        }

        public ICommand StatisticsGameCommand { get; }
        public ICommand NewGameCommand { get; }

        public ICommand PressKey { get; }

        public int attempts = 5;

        private int loses;

        private int score;
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                if (value != score)
                {
                    score = value;
                    NotifyPropertyChanged("Score");
                }
            }
        }

        private int level = 0;

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                if (level != value)
                {
                    level = value;
                    NotifyPropertyChanged("Level");
                }
            }
        }

        public int Attempts
        {
            get
            {
                return attempts;
            }
            set
            {
                if (value != attempts)
                {
                    attempts = value;
                    NotifyPropertyChanged("Attempts");
                }
            }
        }

        public RelayCommand ResetGameCommand { get; }
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

        private void statisticsGameCommand(object e)
        {
            MessageBox.Show("Your level is: " + UserModel.currentUser.Level + '\n' + "Your loses are: " + UserModel.currentUser.Loses);
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimerBar++;
            if (TimerBar >= 30)
            {
                dispatcherTimer.Stop();
                TimerBar = 30;
                isFinished = true;
                Score = 0;
                NotifyPropertyChanged("Score");
                usedLetters.Clear();
                UserModel.currentUser.Loses++;
                Loses = UserModel.currentUser.Loses;
                MessageBox.Show("Time is up!");
                resetGameCommand(sender);
                timerBar = 0;
                dispatcherTimer.Stop();
            }
        }

        public bool isFinished = false;
        void pressKey(object e)
        {
            if (attempts != 0 || isFinished == false) // || && reminder
            {
                if (e is string s)
                {
                    if (Word == "")
                    {
                        dispatcherTimer.Stop();
                        MessageBox.Show("You didn't select any category!");
                    }
                    else
                    {
                        dispatcherTimer.Start();
                        bool ok = false;
                        for (int index = 0; index < letterList.Count; ++index)
                        {
                            if (s[0] == char.ToUpper(letterList[index].RealLetter))
                            {
                                letterList[index].IsHide = false;
                                ok = true;
                                NotifyPropertyChanged("Word");
                            }
                        }
                        usedLetters.Add(s[0]);
                        if (ok == false)
                            Attempts--;
                        int nr = 0;
                        for (int index = 0; index < letterList.Count; ++index)
                        {
                            if (letterList[index].IsHide == false)
                            {
                                nr++;
                            }
                        }
                        if (nr == letterList.Count)
                        {
                            isFinished = true;
                            dispatcherTimer.Stop();
                            UserModel.currentUser.Score++;
                            score = UserModel.currentUser.Score;
                            if (score == 2)
                            {
                                UserModel.currentUser.Score = 0;
                                score = 0;
                                Level++;
                                UserModel.currentUser.Level = Level;
                            }
                            NotifyPropertyChanged("Score");
                            NotifyPropertyChanged("Level");
                            MessageBox.Show("You won!");
                            usedLetters.Clear();
                            resetGameCommand(e);
                            timerBar = 0;
                            dispatcherTimer.Stop();

                        }
                        if (attempts == 0)
                        {
                            dispatcherTimer.Stop();
                            score = UserModel.currentUser.Score = 0;
                            NotifyPropertyChanged("Score");
                            usedLetters.Clear();
                            UserModel.currentUser.Loses++;
                            loses = UserModel.currentUser.Loses;
                            MessageBox.Show("You lost!");
                            resetGameCommand(e);
                            timerBar = 0;
                            dispatcherTimer.Stop();

                        }
                    }
                }
            }
        }

        string realWord;
        void newGameCommand(object e)
        {
            if (e is string Category)
            {
                dispatcherTimer.Start();
                Random random = new Random();
                realWord = "";
                if (Category == "Cars")
                {
                    List<string> Cars = new List<string>()
                    {
                        "Honda Civic",
                        "Dacia Logan",
                        "Golf 4",
                        "Prius",
                        "Renault Symbol"
                   };
                    realWord = Cars[random.Next(0, Cars.Count)];
                }

                if (Category == "Anime")
                {
                    List<string> Anime = new List<string>()
                    {
                        "Naruto",
                        "Bleach",
                        "One Punch Man",
                        "Seven Deadly Sins",
                        "Mob Psycho"
                   };
                    realWord = Anime[random.Next(0, Anime.Count)];
                }
                if (Category == "All categories")
                {
                    List<string> allCategories = new List<string>()
                    {
                        "Naruto",
                        "Bleach",
                        "One Punch Man",
                        "Seven Deadly Sins",
                        "Mob Psycho",
                        "Honda Civic",
                        "Dacia Logan",
                        "Golf 4",
                        "Prius",
                        "Renault Symbol"
                   };
                    realWord = allCategories[random.Next(0, allCategories.Count)];
                }
                letterList.Clear();
                foreach (char letter in realWord)
                {
                    if (letter == ' ')
                        letterList.Add(new Letter(letter, false));
                    else
                        letterList.Add(new Letter(letter, true));
                }
                NotifyPropertyChanged("Word");
            }
        }
    }
}
