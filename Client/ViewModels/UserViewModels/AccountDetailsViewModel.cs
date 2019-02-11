using Client.Access;
using Client.Helpers;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Client.ViewModels.UserViewModels
{
    public class AccountDetailsViewModel
    {
        public User User { get; set; }
        public string PreviousUsername { get; set; }
        public string PreviousPassword { get; set; }
        private static readonly Object lockObj = new object();
        public ObservableCollection<Client.Model.Image> AllImages { get; set; }

        public ICommand EditProfileCommand { get; set; }

        public AccountDetailsViewModel(User u, ObservableCollection<Client.Model.Image> AllImages)
        {
            this.User = u;
            this.PreviousUsername = this.User.Username;
            this.PreviousPassword = this.User.Password;
            this.AllImages = AllImages;
            this.EditProfileCommand = new RelayCommand(EditProfileExecute, EditProfileCanExecute);
        }

        public bool EditProfileCanExecute(object parametar)
        {
            if (User.Username == PreviousUsername && User.Password == PreviousPassword)
                return false;

            return true;
        }

        public void EditProfileExecute(object parametar)
        {
            try
            {
                User.Validate();
                if (User.IsValid)
                {
                    User.Images = this.AllImages;
                    lock (lockObj)
                    {
                        DBManager.Instance.ModifyUser(this.User, this.PreviousUsername);
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
