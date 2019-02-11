using Client.Access;
using Client.Helpers;
using Client.Model;
using Client.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class LoginViewModel
    {
        public User User { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        private static readonly Object lockObj = new object();

        public LoginViewModel()
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.LastIndexOf("bin")) + "Database";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            this.User = new User();
            this.LoginCommand = new RelayCommand(LoginExecute, LoginCanExecute);
            this.RegisterCommand = new RelayCommand(RegisterExecute, RegisterCanExecute);
        }

        public bool LoginCanExecute(object parametar)
        {
            return true;
        }

        public void LoginExecute(object parametar)
        {
            Object[] parametars = parametar as Object[];

            string usrName = parametars[0] as String;
            string passW = parametars[1] as String;
            UserControl usrControl = parametars[2] as UserControl;

            this.User.Username = usrName;
            this.User.Password = passW;
            User user = null;

            User.Validate();
            if (User.IsValid)
            {
                lock (lockObj)
                {


                    user = DBManager.Instance.GetRegistratedUserByName(this.User.Username);
                }

                if (user == null)
                {
                    MessageBox.Show("Invalid username or password");
                }
                else
                {
                    Window homePage = new Window();
                    homePage.Height = 700;
                    homePage.Width = 600;
                    homePage.Content = new HomepageViewModel(this.User);
                    homePage.Show();
                    Window currentWindow = Window.GetWindow(usrControl);
                    currentWindow.Close();
                }
            }

        }

        public bool RegisterCanExecute(object parametar)
        {
            return true;
        }

        public void RegisterExecute(object parametar)
        {
            Object[] parametars = parametar as Object[];

            string usrName = parametars[0] as String;
            string passW = parametars[1] as String;
            UserControl usrControl = parametars[2] as UserControl;

            Window homePage = new Window();
            homePage.Height = 700;
            homePage.Width = 700;
            homePage.Content = new RegisterViewModel();
            homePage.Show();
            Window currentWindow = Window.GetWindow(usrControl);
            currentWindow.Close();
        }
    }
}
