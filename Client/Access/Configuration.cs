using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using Client.Model;

namespace Client.Access
{
    class Configuration : DbMigrationsConfiguration<PhotoGalleryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "PhotoGalleryContext";
        }
    }
}
