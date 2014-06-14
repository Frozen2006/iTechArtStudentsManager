using DAL.Entities;
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
            var group = _context.Groups.FirstOrDefault(m=>m.Name == groupName);
            return new GroupAssignList()
            {
                Assigned = group.Members.Select(m=> new AppUserData() { 
                    UserName = m.UserName, 
                    LastAndFirstName = m.FirstName+' '+m.LastName }).ToArray(),
                Unassigned = _context.Users.Where(m=> !m.Groups.Contains(group)).Select(m=> new AppUserData() { 
                    UserName = m.UserName, 
                    LastAndFirstName = m.FirstName+' '+m.LastName }).ToArray()
            };
        }

        public void CreateGroup(string groupName)
        {
            Group group = new Group()
            {
                Name = groupName
            };

            _context.Groups.Add(group);

            _context.SaveChanges();
        }


        public void UnassignUser(string groupName, string userName)
        {
            var group = _context.Groups.FirstOrDefault(m=>m.Name == groupName);
            _context.Users.FirstOrDefault(m => m.UserName == userName).Groups.Remove(group);


            _context.SaveChanges();
        }
        public void AssignUser(string groupName, string userName)
        {
            var group = _context.Groups.FirstOrDefault(m => m.Name == groupName);
            _context.Users.FirstOrDefault(m => m.UserName == userName).Groups.Add(group);


            _context.SaveChanges();
        }
    }
}
