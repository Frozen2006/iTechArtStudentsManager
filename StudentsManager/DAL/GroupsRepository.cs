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
        StudentsManagerDbContext _context = StudentsManagerDbContext.GetInstance();


        public string[] GetAvalableGroups()
        {
            return _context.Groups.Select(m => m.Name).ToArray();
        }

        public GroupAssignList GetGroupAssignList(string groupName)
        {
            var group = _context.Groups.FirstOrDefault(m=>m.Name == groupName);

            var assignedEntities = group.Members.ToArray().Where(m => IsInRole(m, "Student"));
            var assigned = assignedEntities.Select(m => new AppUserData()
            { 
                    UserName = m.UserName, 
                    LastAndFirstName = m.FirstName+' '+m.LastName }).ToArray();

            var unassigned1 = _context.Users.ToList().Where(m => !assignedEntities.Contains(m)).ToList().Where(m=> IsInRole(m, "Student"));

            var unassigned = unassigned1.Select(m=> new AppUserData() { 
                    UserName = m.UserName, 
                    LastAndFirstName = m.FirstName+' '+m.LastName }).ToArray();

            return new GroupAssignList()
            {
                Assigned = assigned,
                Unassigned = unassigned
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

        private bool IsInRole(ApplicationUser user, string role)
        {
            return _context.Roles.FirstOrDefault(m => m.Name == role).Users.Where(m => m.UserId == user.Id).ToList().Count() > 0;
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


        public string GetGroupSchedule(string groupName)
        {
            return _context.Groups.FirstOrDefault(m => m.Name == groupName).Schedule;
        }

        public void SaveGroupSchedule(string groupName, string value)
        {
            _context.Groups.FirstOrDefault(m => m.Name == groupName).Schedule = value;
            _context.SaveChanges();
        }
    }
}
