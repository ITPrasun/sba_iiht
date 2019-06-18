using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSE_ProjMgr.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public int? ParentID { get; set; }
        public int? ProjectID { get; set; }
        public string TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Priority { get; set; }
        public int Status { get; set; }
        public User User { get; set; }
        public string ParentTaskName { get; set; }
    }
}