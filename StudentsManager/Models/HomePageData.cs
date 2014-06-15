using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HomePageData
    {
        public int CompleatedTasksCount { get; set; }
        public int NewTaskCount { get; set; }
        public string[] Groups { get; set; }
        public string Schedule { get; set; }


    }
}
