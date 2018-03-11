using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using LMSApp.Manager;
using LMSApp.Models;
using LMSApp.Report;
using LMSApp.ViewModel;

namespace LMSApp.Controllers
{
    public class EmployeeLeaveController : Controller
    {
        EmployeeManager eManager = new EmployeeManager();
        ValidationManager vManager = new ValidationManager();

        public ActionResult LeaveTaken()
        {
            ViewBag.Employees = eManager.EmployeeList();
            ViewBag.LeaveTypes = eManager.LeaveTypes();
            return View();
        }

        [HttpPost]
        public ActionResult LeaveTaken(LeaveTaken allocateClassroom)
        {

            if (vManager.IsLeaveTaken(allocateClassroom))
            {
                ViewBag.Msg = "Date Overlapping Problem.";
                ViewBag.Employees = eManager.EmployeeList();
                ViewBag.LeaveTypes = eManager.LeaveTypes();

            }
            else
            {
                allocateClassroom.EntryDate = DateTime.Now;
                ViewBag.Msg = eManager.LeaveTaken(allocateClassroom);
                ViewBag.Employees = eManager.EmployeeList();
                ViewBag.LeaveTypes = eManager.LeaveTypes();
            }

            return View();

        }

        public JsonResult GetEmployeeName(int employeeId)
        {

            var employeeName = eManager.EmployeeList(employeeId);
            return Json(employeeName.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalLeaveInfo(int employeeId)
        {

            var leaveInfo = eManager.LeaveInfo(employeeId);
            return Json(leaveInfo.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalCasualLeave(int employeeId)
        {

            var leaveInfo = eManager.CasualLeaveInfo(employeeId);
            return Json(leaveInfo.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRemaingSickLeave(int employeeId)
        {

            var leaveInfo = eManager.SickLeaveLeft(employeeId);
            return Json(leaveInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRemaingCasualLeave(int employeeId)
        {

            var leaveInfo = eManager.CasualLeaveLeft(employeeId);
            return Json(leaveInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Report()
        {
            ViewBag.departments = eManager.EmployeeList();
            return View();
        }
        [HttpPost]
        public ActionResult Report(EmployeeLeaveReport leaveTaken)
        {
            LeaveReport report = new LeaveReport();
            var employees = LeaveTakens(leaveTaken);
            byte[] abytes = report.PrePareReport(employees, leaveTaken);
            return File(abytes, "application/pdf");
        }

        public List<EmployeeLeaveReport> LeaveTakens(EmployeeLeaveReport employeeId)
        {

            return eManager.EmployeeLeaveList(employeeId.EmployeeId);
        }
        public JsonResult GetEmployeeLeaveInfo(int departmentId)
        {
           var employeeLeaveList = eManager.EmployeeLeaveList(departmentId);
           return Json(employeeLeaveList.Where(x => x.EmployeeId == departmentId).Select(x => new
            {
                EmployeeId = x.EmployeeId,
                Name = x.EmployeeName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                TotalDays = x.NoOfLeaveTake,
                EntryDate = x.EntryDate,
                LeaveTypeName = x.LeaveType

            }).ToList(), JsonRequestBehavior.AllowGet);
        }
 
    }
}