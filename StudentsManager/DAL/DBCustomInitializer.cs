using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
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
                var studentRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Student"
                };
                context.Roles.Add(studentRole);

                var teacherRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Teacher"
                };
                context.Roles.Add(teacherRole);

                var managerRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Manager"
                };
                context.Roles.Add(managerRole);

                context.SaveChanges();

                ApplicationUser testUser = new ApplicationUser
                {
                    UserName = "test",
                    PasswordHash = "AAv8jUAGu/+mXbigKaT0q2jOKKqKU9dSDUKACiYnTZeKLMyohIg8oMr/9w7AyigS3A==",
                    SecurityStamp = "81a738ef-6b6a-4bd5-b6d0-442fb2f903e6"
                };

                context.Users.Add(testUser);
                context.SaveChanges();

                testUser.Roles.Add(new IdentityUserRole
                {
                    RoleId = teacherRole.Id,
                    UserId = testUser.Id
                });

                context.SaveChanges();
            }
            
        }

        
    }
}
