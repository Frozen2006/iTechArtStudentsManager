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
    
    public partial class Lection
    {
        public Lection()
        {
            this.Files = new HashSet<File>();
            this.Groups = new HashSet<Group>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatorId { get; set; }
    
        public virtual ICollection<File> Files { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
