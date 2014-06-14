using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GroupsRepository
    {
        StudentsManagerDbContext _context = new StudentsManagerDbContext();


        public string[] GetAvalableGroups()
        {
            return _context.Groups.Select(m => m.Name).ToArray();
        }

        public GroupAssignList GetGroupAssignList(string groupName)
        {
           /* var group = _context.Groups.FirstOrDefault(m=>m.Name == groupName);
            return new GroupAssignList()
            {
                Assigned = group.
            };*/
        }

        public void CreateGroup(string groupName)
        {
            //
        }


        public void UnassignUser(string groupName, string userName)
        {
        }
        public void AssignUser(string groupName, string userName)
        {

        }
    }
}
