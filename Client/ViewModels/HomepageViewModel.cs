using Client.Helpers;
using Client.Model;
using Client.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Client.Access;

namespace Client.ViewModels
{
    public class HomepageViewModel
    {
        public User User { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ObservableCollection<object> viewModels { get; set; }
        public ObservableCollection<Client.Model.Image> AllImages { get; set; }

        public HomepageViewModel(User u)
        {
            this.User = u;
            this.User = DBManager.Instance.GetAllImagesForUser(this.User.Username);
            this.LogOutCommand = new RelayCommand(LogOutExecute, LogOutCanExecute);
            AllImages = new ObservableCollection<Client.Model.Image>(this.User.Images);

            viewModels = new ObservableCollection<object>();
            viewModels.Add(new MyImagesViewModel(this.User, AllImages));
            viewModels.Add(new AddImageViewModel(this.User, AllImages));
            viewModels.Add(new AccountDetailsViewModel(this.User, AllImages));
        }

        public bool LogOutCanExecute(object parametar)
        {
            return true;
        }

        public void LogOutExecute(object parametar)
        {
            Window login = new Window();
            login.Height = 800;
            login.Width = 620;
            login.Content = new LoginViewModel();
            login.Show();

            UserControl uc = parametar as UserControl;

            Window Window = Window.GetWindow(uc);
            Window.Close();
        }
    }
}

