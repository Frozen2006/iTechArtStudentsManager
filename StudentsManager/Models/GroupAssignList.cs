using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GroupAssignList
    {
        public AppUserData[] Assigned { get; set; }
        public AppUserData[] Unassigned { get; set; }
    }
}
