using Client.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class User : ValidationBase
    {
        private string username;
        private string password;

        [Key]
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username == value)
                    return;

                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        [Required]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password == value)
                    return;

                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.Username))
            {
                this.ValidationErrors["Username"] = "Username is required.";
            }
            if (string.IsNullOrWhiteSpace(this.Password))
            {
                this.ValidationErrors["Password"] = "Password is required";
            }
            if (this.Password != "" && this.Password.Length < 6)
            {
                this.ValidationErrors["Password"] = "Password must be at least 6 characters long.";
            }
            if(this.Username != "" && char.IsDigit(this.Username[0]))
            {
                this.ValidationErrors["Username"] = "First character of username cannot be digit.";
            }
            
        }

        [IgnoreDataMember]
        public ICollection<Image> Images { get; set; }
    }
}
