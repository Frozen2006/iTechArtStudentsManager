using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime? Birthday { get; set; }
        public string Institution { get; set; }
        public string Faculty { get; set; }
        public string Major { get; set; }
        public Nullable<int> Student { get; set; }


        public virtual ICollection<Lection> Lections { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Notification> SentNotifications { get; set; }
    }
}
