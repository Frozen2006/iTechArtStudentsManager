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


            foreach(var tag in tags)
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

            return context.TaskStudents.Where(m => m.Task != task) //select students who didn't has this task
                .Select(m => m.Student).ToList()
                .Select(m => new AppUserData() { LastAndFirstName = m.FirstName + ' ' + m.LastName, UserName = m.UserName }).ToArray();
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
    }
}
