//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notification
    {
        public Notification()
        {
            this.Receivers = new HashSet<ApplicationUser>();
        }
    
        public int Id { get; set; }
        public string Content { get; set; }

        public virtual ICollection<ApplicationUser> Receivers { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }
}