using DAL.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TaskRepository
    {
        StudentsManagerDbContext context = new StudentsManagerDbContext();


        public TagsList[] GetTaskList()
        {
            List<TagsList> data = new List<TagsList>();

            var tags = context.Tasks.Select(m => m.Tags).Distinct().ToArray();


            foreach (var tag in tags)
            {
                TagsList list = new TagsList()
                {
                    Tag = tag,
                    Tasks = context.Tasks.Where(m => m.Tags == tag).ToList().Select(m => new TaskDispayModel() { Tag = tag, Title = m.Name, ComplexLevel = m.Level.ToString() }).ToArray()
                };
                data.Add(list);
            }


            return data.ToArray();
        }

        public string GetTaskContent(string title, string tag)
        {
            return context.Tasks.FirstOrDefault(m => (m.Name == title) && (m.Tags == tag)).Desciption;
        }


        public void CreateTask(TaskDispayModel model)
        {
            var task = new Entities.Task()
            {
                CreationDate = DateTime.Now,
                Desciption = model.Content,
                Name = model.Title,
                Level = Convert.ToInt32(model.ComplexLevel),
                Tags = model.Tag
            };

            context.Tasks.Add(task);

            context.SaveChanges();
        }

        public AppUserData[] GetStudentsAvalableForTask(string taskTitle, string taskTag)
        {
            var task = context.Tasks.FirstOrDefault(m => (m.Name == taskTitle) && (m.Tags == taskTag));
            var studentRoleId = context.Roles.FirstOrDefault(m => m.Name == "Student").Id;

            var alreadyAssignbed = context.TaskStudents.Where(m => m.Task.Id == task.Id).Select(m => m.Student.Id).ToList();

            var usersId = context.Users.Select(m => m.Id).ToList().Where(m => !alreadyAssignbed.Contains(m));

            return context.Users.ToList().Where(m => usersId.Contains(m.Id)).ToList().Where(q => IsInRole(q, "Student"))
                .Select(m => new AppUserData() { LastAndFirstName = m.FirstName + ' ' + m.LastName, UserName = m.UserName }).ToArray();
        }


        private bool IsInRole(ApplicationUser user, string role)
        {
            var a = context.Roles.FirstOrDefault(m => m.Name == role);
            var b = a.Users.Where(m => m.UserId == user.Id);
            return context.Roles.FirstOrDefault(m => m.Name == role).Users.Where(m => m.UserId == user.Id).Count() > 0;
        }

        public void AssignTaskToUser(string taskTitle, string taskTag, string userName)
        {
            var task = context.Tasks.FirstOrDefault(m => (m.Name == taskTitle) && (m.Tags == taskTag));

            TaskStudent trelation = new TaskStudent()
            {
                Student = context.Users.FirstOrDefault(m => m.UserName == userName),
                Task = task
            };

            context.TaskStudents.Add(trelation);

            context.SaveChanges();
        }


        public StudentMark[] GetUserMarks(string userName)
        {
            return context.TaskStudents.Where(m => m.Student.UserName == userName).ToArray().
                Where(m => m.Mark != null).Select(m => new StudentMark() { Tag = m.Task.Tags, Mark = (int)m.Mark }).ToArray();
        }

        public AppUserData[] GetStudentsWithTasks()
        {
            return context.TaskStudents.Where(m=>m.Mark == null).Select(m => m.Student).Distinct().ToList()
                .Select(m => new AppUserData() { LastAndFirstName = m.FirstName + ' ' + m.LastName, UserName = m.UserName }).ToArray();
        }


        public TaskDispayModel[] GetUserTasks(string userName)
        {
            return context.TaskStudents.Where(m => m.Student.UserName == userName).ToList()
                .Select(m => new TaskDispayModel() { Title = m.Task.Name, Tag = m.Task.Tags, ComplexLevel = m.Task.Level.ToString(), Content = m.Task.Desciption }).ToArray();
        }


        public void SaveTaskResults(string userName, string taskTitle, string taskTag, string taskMark, string taskComent)
        {
            var task = context.TaskStudents.FirstOrDefault(m => (m.Task.Name == taskTitle) && (m.Task.Tags == taskTag) && (m.Student.UserName == userName));

            task.Mark = Convert.ToInt32(taskMark);
            task.Comments.Add(new Comment()
            {
                Content = taskComent,
                IsVisible = true
            });

            context.SaveChanges();
        }
    }
}
