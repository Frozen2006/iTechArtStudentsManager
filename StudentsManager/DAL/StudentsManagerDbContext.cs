﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

using DAL.Entities;

namespace DAL
{
    public class StudentsManagerDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<File> Files { get; set; }
        public virtual IDbSet<Group> Groups { get; set; }
        public virtual IDbSet<Lection> Lections { get; set; }
        public virtual IDbSet<Notification> Notifications { get; set; }
       
        public virtual IDbSet<DAL.Entities.Task> Tasks { get; set; }
        public virtual IDbSet<TaskStudent> TaskStudents { get; set; }

        public StudentsManagerDbContext()
            : base("StudentsManagerDb")
        {
            Database.SetInitializer<StudentsManagerDbContext>(new DBCustomInitializer());
            Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

        }

        private static StudentsManagerDbContext _instance = null;

        public static StudentsManagerDbContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StudentsManagerDbContext();
            }

            return _instance;
        }


    }
}
