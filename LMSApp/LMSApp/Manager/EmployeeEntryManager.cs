using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSApp.Gateway;
using LMSApp.Models;

namespace LMSApp.Manager
{
    public class EmployeeEntryManager
    {
        private EmployeeEntryGateway eGateway = new EmployeeEntryGateway();
        public List<Designation> DesignationList()
        {
            return eGateway.DesignationList();
        }

        public string SaveEmployee(Employee employee)
        {
            return eGateway.SaveEmployee(employee);
        }

        public bool IsEmailExists(string email)
        {
            return eGateway.IsEmailExist(email);
        }
    }
}