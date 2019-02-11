using Client.Access;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels.UserViewModels
{
    public class MyImagesViewModel
    {
        public User User { get; set; }
        private static readonly Object lockObj = new object();
        public ObservableCollection<Image> AllImages { get; set; }

        public MyImagesViewModel(User u, ObservableCollection<Image> AllImages)
        {
            this.User = u;
            this.AllImages = AllImages;
        }
    }
}
