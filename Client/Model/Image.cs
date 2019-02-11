using Client.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class Image : ValidationBase
    {
        private string path;
        private string title;
        private string description;
        private DateTime addedAt;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (path == value)
                    return;

                path = value;
                OnPropertyChanged(nameof(Path));
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title == value)
                    return;

                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description == value)
                    return;

                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public DateTime AddedAt
        {
            get
            {
                return addedAt;
            }
            set
            {
                if (addedAt == value)
                    return;

                addedAt = value;
                OnPropertyChanged(nameof(AddedAt));
            }
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
            {
                this.ValidationErrors["Title"] = "Title is required.";
            }
            if (string.IsNullOrWhiteSpace(this.Path))
            {
                this.ValidationErrors["Path"] = "Path is required.";
            }
        }
    }
}
