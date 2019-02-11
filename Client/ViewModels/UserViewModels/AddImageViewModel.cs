using Client.Access;
using Client.Helpers;
using Client.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client.ViewModels.UserViewModels
{
    public class AddImageViewModel : INotifyPropertyChanged
    {
        public User User { get; set; }
        private String Path { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand LoadImageCommand { get; set; }
        private ImageSource uploadedImage;
        private static readonly Object lockObj = new object();
        public ObservableCollection<Image> AllImages { get; set; }
        public Image Image { get; set; }

        public ImageSource UploadedImage
        {
            get
            {
                return uploadedImage;
            }
            set
            {
                if (uploadedImage == value)
                    return;

                uploadedImage = value;
                OnPropertyChanged(nameof(UploadedImage));
            }
        }


        public AddImageViewModel(User u, ObservableCollection<Image> AllImages)
        {
            this.Image = new Image();
            this.User = u;
            this.AddImageCommand = new RelayCommand(AddImageExecute, AddImageCanExecute);
            this.LoadImageCommand = new RelayCommand(LoadImageExecute, LoadImageCanExecute);
            this.AllImages = AllImages;
        }

        public bool LoadImageCanExecute(object parametar)
        {
            return true;
        }

        public void LoadImageExecute(object parametar)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                if (File.Exists(op.FileName))
                {
                    Path = op.FileName;
                    UploadedImage = new BitmapImage(new Uri(op.FileName, UriKind.Absolute));
                }
            }
        }

        public bool AddImageCanExecute(object parametar)
        {
            return true;
        }

        public void AddImageExecute(object parametar)
        {
            Object[] parametars = parametar as Object[];

            String title = parametars[0] as String;
            String description = parametars[1] as String;
            ImageSource uploadedPicSource = parametars[2] as ImageSource;

            Image.Title = title;
            Image.Description = description;
            Image.Path = Path;

            Image image = new Image();
            image.ImageID = 0;
            image.Title = title;
            image.Path = Path;
            image.Description = description;
            image.AddedAt = DateTime.Now;

            try
            {
                Image.Validate();
                if (Image.IsValid)
                {
                    lock (lockObj)
                    {
                        DBManager.Instance.AddNewImage(User, image);
                    }
                    this.AllImages.Add(image);
                    this.UploadedImage = null;
                    this.Path = null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
