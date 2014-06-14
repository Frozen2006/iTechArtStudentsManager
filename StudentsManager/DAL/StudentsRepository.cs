using DAL.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StudentsRepository
    {
        StudentsManagerDbContext context = new StudentsManagerDbContext();


        public AppUserData[] GetUsersNamesList()
        {
            return context.Users.Select(m => new  AppUserData()
            {
                LastAndFirstName = m.FirstName +" " + m.LastName,
                UserName = m.UserName
            }).ToArray();
        }

        public StudentMark[] GetStudentsMarks(string userName)
        {
            return context.TaskStudents.Where(m => m.Student.UserName == userName).Where(m => m.Mark != null).Select(m => new StudentMark()
                {
                    Mark = (int)m.Mark,
                    Tag = m.Task.Tags
                }).ToArray();
        }
    }
}
