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

                CreateTasks(context, testUser);
            }
            
        }

        private void CreateTasks(StudentsManagerDbContext context, ApplicationUser student)
        {
            var task1 = new Entities.Task
            {
                CreationDate = DateTime.Now,
                Name = "Task1",
                Desciption = "This is Task1",
                Level = 4,
                Tags = "javascript, html, css"
            };
            context.Tasks.Add(task1);
            


            var task2 = new Entities.Task
            {
                CreationDate = DateTime.Now,
                Name = "Task2",
                Desciption = "This is Task2",
                Level = 8,
                Tags = "C#, ASP"
            };
            context.Tasks.Add(task2);
            
            var task3 = new Entities.Task
            {
                CreationDate = DateTime.Now,
                Name = "Task3",
                Desciption = "This is Task3",
                Level = 2,
                Tags = "reading"
            };
            context.Tasks.Add(task3);
            

            context.SaveChanges();


            context.TaskStudents.Add(new TaskStudent
            {
                Student = student,
                Task = task1
            });

            context.TaskStudents.Add(new TaskStudent
            {
                Student = student,
                Task = task2
            });

            context.TaskStudents.Add(new TaskStudent
            {
                Student = student,
                Task = task3
            });

            context.SaveChanges();
        }

        

        
    }
}
