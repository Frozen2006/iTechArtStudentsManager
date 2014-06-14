using System;
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
        public virtual IDbSet<LectionGroup> LectionGroups { get; set; }
        public virtual IDbSet<Notification> Notifications { get; set; }
        public virtual IDbSet<NotificationReceivers> NotificationReceivers { get; set; }
        public virtual IDbSet<NotificationSenders> NotificationSenders { get; set; }
        public virtual IDbSet<DAL.Entities.Task> Tasks { get; set; }
        public virtual IDbSet<TaskStudent> TaskStudents { get; set; }
        public virtual IDbSet<UserGroup> UserGroups { get; set; }

        public StudentsManagerDbContext()
            : base("StudentsManagerDb")
        {
            Database.SetInitializer<StudentsManagerDbContext>(new CreateDatabaseIfNotExists< StudentsManagerDbContext>() );
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<NotificationSenders>()
                .HasOptional(f => f.Notification)
                .WithRequired(s => s.NotificationSender);
        }


    }
}
