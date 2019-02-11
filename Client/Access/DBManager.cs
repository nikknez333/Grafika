using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Client.Access
{
    class DBManager
    {
        private static DBManager instance;

        private DBManager()
        {

        }

        public static DBManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBManager();
                }

                return instance;
            }
        }

        public bool UserExist(string username)
        {
            using (var dbContext = new PhotoGalleryContext())
            {
                bool exist = false;

                if (dbContext.Users.Any(u => u.Username == username))
                    exist = true;

                return exist;
            }
        }

        public bool AddNewUser(User u)
        {
            using (var dbContext = new PhotoGalleryContext())
            {
                bool IsInDB = dbContext.Users.Any(usr => usr.Username == u.Username);

                if (IsInDB)
                    return false;

                dbContext.Users.Add(u);

                dbContext.SaveChanges();

                return true;
            }
        }

        public User GetRegistratedUserByName(string username)
        {
            using(var dbContext = new PhotoGalleryContext())
            {
                return dbContext.Users.Find(username);
            }
        }

        public User GetAllImagesForUser(string username)
        {
            using (var dbContext = new PhotoGalleryContext())
            {
                User user = dbContext.Users.Include(usr => usr.Images).FirstOrDefault(usrName => usrName.Username == username);

                return user;
            }
        }

        public void ModifyUser(User  u, string userToModify)
        {
            using(var dbContext = new PhotoGalleryContext())
            {
                User user = dbContext.Users.Include(usr=> usr.Images).FirstOrDefault(usr => usr.Username == userToModify);

                if (user == null)
                    return;

                dbContext.Images.RemoveRange(user.Images);
                dbContext.Users.Remove(user);

                User modifiedUser = new User();
                modifiedUser.Username = u.Username;
                modifiedUser.Password = u.Password;
                modifiedUser.Images = u.Images;
                dbContext.Images.AddRange(modifiedUser.Images);
                dbContext.Users.Add(modifiedUser);
               
                dbContext.SaveChanges();
            }
        }

        public void AddNewImage(User u, Image img)
        {
            using (var dbContext = new PhotoGalleryContext())
            {
                bool IsInDB = dbContext.Images.Any(imag => imag.ImageID == img.ImageID);

                if (IsInDB)
                    return;

                dbContext.Images.Add(img);

                User user = dbContext.Users.Include(usrImg => usrImg.Images).FirstOrDefault(usr => usr.Username == u.Username);

                if (user.Images.Contains(img))
                    return;

                user.Images.Add(img);

                dbContext.SaveChanges();
            }
        }
    }
}
