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
            StudentMark[] marks = null;

            await Task.Run(() =>
            {
                marks = _tasks.GetUserMarks(studentName);
            });


            return marks;
        }

        public StudentMark[] GetStudentMarksSync(string studentName)
        {
                return _tasks.GetUserMarks(studentName);
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



        public async Task<string> GetGroupSchedule(string groupName)
        {
            string data = null;
            await Task.Run(() => 
            {
                data = _groups.GetGroupSchedule(groupName);
            });

            return data;
        }

        public async Task SaveGroupSchedule(string groupName, string value)
        {
            _groups.SaveGroupSchedule(groupName, value);
        }


        public async Task<AppUserData[]> GetStudentsWithTasks()
        {
            AppUserData[] users = null;

            await Task.Run(() =>
            {
                users = _tasks.GetStudentsWithTasks();
            });


            return users;
        }


        public async Task<TaskDispayModel[]> GetUserTasks(string userName)
        {
            TaskDispayModel[] tasks = null;

            await Task.Run(() =>
            {
                tasks = _tasks.GetUserTasks(userName);
            });


            return tasks;
        }



        public async Task SaveTaskResults(string userName, string taskTitle, string taskTag, string taskMark, string taskComent)
        {
            _tasks.SaveTaskResults(userName, taskTitle, taskTag, taskMark, taskComent);
        }


        public HomePageData GetHomePageData(string userName)
        {
            return _repository.GetHomePageData(userName);
        }

        public async Task<string[]> GetCurrentTasksNames(string userName)
        {
            string[] tasksNames = null;

            await Task.Run(() =>
            {
                tasksNames = _tasks.GetCurrentTasksNames(userName);
            });

            return tasksNames;
        }

        public async Task<Models.TaskDispayModel> GetTaskDetails(string taskName)
        {
            TaskDispayModel result = null;

            var task = _tasks.GetTaskDetails(taskName);

            return task;
        }



        
    }
}