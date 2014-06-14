using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBCustomInitializer : CreateDatabaseIfNotExists<StudentsManagerDbContext>
    {
        public override void InitializeDatabase(StudentsManagerDbContext context)
        {
            base.InitializeDatabase(context);
            if (context.Roles.Count() < 3)
            {
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Student"
                });
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Teacher"
                });
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Manager"
                });


                context.SaveChanges();
            }
        }
    }
}
