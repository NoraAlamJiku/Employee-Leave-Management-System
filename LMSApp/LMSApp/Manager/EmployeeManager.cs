using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSApp.Gateway;
using LMSApp.Models;
using LMSApp.ViewModel;

namespace LMSApp.Manager
{
    public class EmployeeManager
    {
        EmployeeGateway eGateway = new EmployeeGateway();

        public string LeaveTaken(LeaveTaken taken)
        {
            return eGateway.LeaveTaken(taken);
        }

        public List<Employee> EmployeeList()
        {
            return eGateway.EmployeeList();
        }

        public List<LeaveType> LeaveTypes()
        {
            return eGateway.LeaveTypes();
        }

        public List<Employee> EmployeeList(int employeeId)
        {
            return eGateway.AllEmployeeList(employeeId);
        }
        public List<AllocatedLeave> LeaveInfo(int employeeId)
        {
            return eGateway.AllLeaveInfo(employeeId);
        }

        public List<AllocatedLeave> CasualLeaveInfo(int employeeId)
        {
            return eGateway.AllCasualLeaveInfo(employeeId);
        }

        public int SickLeaveLeft(int employeeId)
        {
           var totalSickLeave =  eGateway.AllLeaveInfo(employeeId);
           var sickLeaveTaken = eGateway.RemaingSickLeave(employeeId);
           var remaingSickLeave = totalSickLeave.FirstOrDefault().NoOfAllocatedLeave - sickLeaveTaken.FirstOrDefault().NoOfAllocatedLeave;
            return remaingSickLeave;
        }

        public int CasualLeaveLeft(int employeeId)
        {
            var totalSickLeave = eGateway.AllCasualLeaveInfo(employeeId);
            var sickLeaveTaken = eGateway.RemaingCasualLeave(employeeId);
            var remaingSickLeave = totalSickLeave.FirstOrDefault().NoOfAllocatedLeave - sickLeaveTaken.FirstOrDefault().NoOfAllocatedLeave;
            return remaingSickLeave;
        }

        public List<EmployeeLeaveReport> EmployeeLeaveList(int employeeId)
        {
            return eGateway.EmployeeLeaveList(employeeId);
        }
    }
}