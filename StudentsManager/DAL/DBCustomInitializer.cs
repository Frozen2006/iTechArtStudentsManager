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



                AddStudents(context, studentRole.Id);

                CreateTasks(context, testUser);

            }
            
        }



        public void AddStudents(StudentsManagerDbContext context, string studntRoleId)
        {
            ApplicationUser testUser1 = new ApplicationUser
            {
                UserName = "stud1",
                PasswordHash = "AAv8jUAGu/+mXbigKaT0q2jOKKqKU9dSDUKACiYnTZeKLMyohIg8oMr/9w7AyigS3A==",
                SecurityStamp = "81a738ef-6b6a-4bd5-b6d0-442fb2f903e6",
                FirstName = "Test",
                LastName = "Us1"
            };
            ApplicationUser testUser2 = new ApplicationUser
            {
                UserName = "stud2",
                PasswordHash = "AAv8jUAGu/+mXbigKaT0q2jOKKqKU9dSDUKACiYnTZeKLMyohIg8oMr/9w7AyigS3A==",
                SecurityStamp = "81a738ef-6b6a-4bd5-b6d0-442fb2f903e6",
                FirstName = "Test",
                LastName = "Us2"
            };
            ApplicationUser testUser3 = new ApplicationUser
            {
                UserName = "stud3",
                PasswordHash = "AAv8jUAGu/+mXbigKaT0q2jOKKqKU9dSDUKACiYnTZeKLMyohIg8oMr/9w7AyigS3A==",
                SecurityStamp = "81a738ef-6b6a-4bd5-b6d0-442fb2f903e6",
                FirstName = "Test",
                LastName = "Us3"
            };

            context.Users.Add(testUser1);
            context.Users.Add(testUser2);
            context.Users.Add(testUser3);

            context.SaveChanges();

            testUser1.Roles.Add(new IdentityUserRole
            {
                RoleId = studntRoleId,
                UserId = testUser1.Id
            });

            testUser2.Roles.Add(new IdentityUserRole
            {
                RoleId = studntRoleId,
                UserId = testUser2.Id
            });

            testUser3.Roles.Add(new IdentityUserRole
            {
                RoleId = studntRoleId,
                UserId = testUser3.Id
            });
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
