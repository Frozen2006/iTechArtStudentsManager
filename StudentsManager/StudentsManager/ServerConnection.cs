using DAL;
using Microsoft.AspNet.SignalR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentsManager
{
    public class ServerConnection : Hub
    {

        private StudentsRepository _repository = new StudentsRepository();
        private GroupsRepository _groups = new GroupsRepository();
        private TaskRepository _tasks = new TaskRepository();

        public async Task<string[]> HelloWorld()
        {
            var data = new List<string>();
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    data.Add("HelloWorld" + i.ToString());
                }
            });

            return data.ToArray();
        }

        Random rand = new Random();
        public async Task<StudentMark[]> GetStudentMarks(string studentName)
        {
            List<StudentMark> marks = new List<StudentMark>();

            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    var model = new StudentMark()
                    {
                        Tag = "JS",
                        Mark = rand.Next(1, 10)
                    };

                    marks.Add(model);
                }

            });


            return marks.ToArray();
        }

        public async Task<AppUserData[]> GetStudents()
        {
            AppUserData[] data = null;
            await Task.Run(() =>
            {
                data = _repository.GetStudentsNamesList();

            });

            return data;
        }


        /*GROUPS*/
        public async Task UnassignUser(string groupName, string userName)
        {
            _groups.UnassignUser(groupName, userName);
        }
        public async Task AssignUser(string groupName, string userName)
        {
            _groups.AssignUser(groupName, userName);
        }
        public async Task CreateGroup(string groupName)
        {
            _groups.CreateGroup(groupName);
        }
        public async Task<GroupAssignList> GetAssignStatusForGroup(string groupName)
        {
            GroupAssignList list = null;

            await Task.Run(() =>
            {
                list = _groups.GetGroupAssignList(groupName);
            });

            return list;
        }
        public async Task<string[]> GetGroups()
        {
            string[] groups = null;

            await Task.Run(() =>
            {
                groups = _groups.GetAvalableGroups();
            });

            return groups;
        }


        public async Task<TagsList[]> GetTaskList()
        {
            TagsList[] list = null;
            await Task.Run(() =>
            {
                list = _tasks.GetTaskList();
            });

            return list;
        }

        public async Task<string> GetTaskText(string title, string tag)
        {
            string content = null;

            await Task.Run(() =>
            {
                content = _tasks.GetTaskContent(title, tag);
            });

            return content;
        }

        public async Task CreateTask(TaskDispayModel model)
        {
            _tasks.CreateTask(model);
        }


        public async Task<AppUserData[]> GetStudentsAvalableForTask(string taskTitle, string taskTag)
        {
            AppUserData[] users = null;

            await Task.Run(() =>
            {
                users = _tasks.GetStudentsAvalableForTask(taskTitle, taskTag);
            });

            return users;
        }

        public async Task AssignTaskToUser(string taskTitle, string taskTag, string userName)
        {
            _tasks.AssignTaskToUser(taskTitle, taskTag, userName);
        }
    }
}