using Client.Access;
using Client.Helpers;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class RegisterViewModel
    {
        public User User { get; set; }
        public ICommand RegisterCommand { get; set; }
        private static readonly Object lockObj = new object();

        public RegisterViewModel()
        {
            this.User = new User();
            this.User.Username = "";
            this.User.Password = "";
            this.RegisterCommand = new RelayCommand(RegisterExecute, RegisterCanExecute);
        }

        public bool RegisterCanExecute(object parametar)
        {
            return true;
        }

        public void RegisterExecute(object parametar)
        {
            Object[] parametars = parametar as Object[];

            UserControl usrControl = parametars[0] as UserControl;
            bool result = false;


            User.Validate();

            if (User.IsValid)
            {

                lock (lockObj)
                {
                    result = DBManager.Instance.AddNewUser(User);
                }

                if (result == false)
                    MessageBox.Show("User with provided username already exist");
                else
                {
                    Window login = new Window();
                    login.Height = 750;
                    login.Width = 620;
                    login.Content = new LoginViewModel();
                    login.Show();
                    Window currentWindow = Window.GetWindow(usrControl);
                    currentWindow.Close();
                }
            }
        }
    }
}
