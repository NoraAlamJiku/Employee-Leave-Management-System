using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSApp.ViewModel
{
    public class EmployeeLeaveReport
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string EntryDate { get; set; }
        public string NoOfLeaveTake { get; set; }
        public string LeaveType { get; set; }
    }
}