using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSApp.Models
{
    public class AllocatedLeave
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int NoOfAllocatedLeave { get; set; }
    }
}