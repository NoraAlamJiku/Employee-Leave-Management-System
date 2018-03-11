using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSApp.Gateway;
using LMSApp.Models;

namespace LMSApp.Manager
{
    public class AllocatedLeaveManager
    {
        AllocatedLeaveGateway eGateway = new AllocatedLeaveGateway();
        public List<Employee> EmployeeList()
        {
            return eGateway.EmployeeList();
        }

        public List<LeaveType> LeaveTypes()
        {
            return eGateway.LeaveTypes();
        }

        public string AllocatedLeave(AllocatedLeave allocated)
        {
            return eGateway.AllocatedLeave(allocated);
        }

        public bool IsLeaveAllocated(AllocatedLeave leaveTaken)
        {
            return eGateway.IsLeaveAllocated(leaveTaken);
        }
    }
}