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
                for(int i=0; i<10; i++)
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




       
    }
}