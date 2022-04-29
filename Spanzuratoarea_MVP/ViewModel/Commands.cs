using Spanzuratoarea_MVP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spanzuratoarea_MVP.ViewModel
{
    internal class Commands
    {
        public ICommand NewUser { get; } = new RelayCommand(newUser);

        static void newUser(object e)
        {
            NewUserWindow newUserWindow = new NewUserWindow();
            newUserWindow.Show();
        }


    }
}
