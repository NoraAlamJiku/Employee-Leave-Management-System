using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSApp.Manager;
using LMSApp.Models;

namespace LMSApp.Controllers
{
    public class AllocatedLeaveController : Controller
    {
        AllocatedLeaveManager eManager = new AllocatedLeaveManager();
        public ActionResult AllocatedLeave()
        {
            ViewBag.Employees = eManager.EmployeeList();
            ViewBag.LeaveTypes = eManager.LeaveTypes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllocatedLeave(AllocatedLeave allocateClassroom)
        {

            if (eManager.IsLeaveAllocated(allocateClassroom))
            {
                ViewBag.Msg = "Leave allocated alrady.";
                ViewBag.Employees = eManager.EmployeeList();
                ViewBag.LeaveTypes = eManager.LeaveTypes();
            }
            else
            {

                ViewBag.Msg = eManager.AllocatedLeave(allocateClassroom);
                ViewBag.Employees = eManager.EmployeeList();
                ViewBag.LeaveTypes = eManager.LeaveTypes();
            }

            return View();

        }
    }
}