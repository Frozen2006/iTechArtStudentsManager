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

            var assignedEntities = group.Members.ToArray();
            var assigned = assignedEntities.Select(m => new AppUserData()
            { 
                    UserName = m.UserName, 
                    LastAndFirstName = m.FirstName+' '+m.LastName }).ToArray();

            var unassigned1 = _context.Users.Where(m => !assignedEntities.Contains(m)).ToList();

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
