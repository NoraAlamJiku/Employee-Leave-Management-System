using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using LMSApp.Models;
using LMSApp.ViewModel;
using Microsoft.Ajax.Utilities;

namespace LMSApp.Gateway
{
    public class EmployeeGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["LeaveManagenent"].ConnectionString;

        public string LeaveTaken(LeaveTaken taken)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(@"INSERT INTO [dbo].[LeaveTaken]
           ([EmployeeId]
           ,[LeaveTypeId]
           ,[StartDate]
           ,[EndDate]
           ,[EntryDate]
           ,[NoOfLeaveTaken])
     VALUES
           ( '" + taken.EmployeeId + "' , '" + taken.LeaveTypeId + "'  , '" + taken.StartDate + "'  , '" + taken.EndDate + "'  , '" + taken.EntryDate + "' , '" + taken.NoOfLeaveTaken + "')", con);
            con.Open();
            int number = com.ExecuteNonQuery();
            con.Close();
            if (number > 0)
            {
                return "Employee Taken Leave Info Save Successfully!";
            }
            return "Employee Taken Leave Info Save Failed, Please Try Again!!!";
        }


        public List<Employee> EmployeeList()
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(@"SELECT * FROM tb_Employees", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<Employee> employeeList = new List<Employee>();
            while (dr.Read())
            {
                Employee emplotee = new Employee();
                emplotee.Id = (int)dr["Id"];
                emplotee.Name = dr["Name"].ToString();
                employeeList.Add(emplotee);
            }
            con.Close();
            return employeeList.ToList();
        }
        public List<LeaveType> LeaveTypes()
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(@"SELECT * FROM LeaveType", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<LeaveType> leaveTypeList = new List<LeaveType>();
            while (dr.Read())
            {
                LeaveType leaveType = new LeaveType();
                leaveType.Id = (int)dr["Id"];
                leaveType.TypeName = dr["TypeName"].ToString();
                leaveTypeList.Add(leaveType);
            }
            con.Close();
            return leaveTypeList.ToList();
        }
        public List<Employee> AllEmployeeList(int employeeId)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(@"SELECT * FROM tb_Employees Where Id ='" + employeeId + "' ", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<Employee> EmployeeNames = new List<Employee>();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.Id = (int)dr["Id"];
                employee.Name = dr["Name"].ToString();
                EmployeeNames.Add(employee);
            }
            con.Close();
            return EmployeeNames.ToList();
        }

        public List<AllocatedLeave> AllLeaveInfo(int employeeId)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(@"SELECT * FROM AllocatedLeave Where EmployeeId ='" + employeeId + "' and LeaveTypeId = '"+ 1 +"' ", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<AllocatedLeave> totalLeave = new List<AllocatedLeave>();
            while (dr.Read())
            {
                AllocatedLeave leave = new AllocatedLeave();
                leave.Id = (int)dr["Id"];
                leave.NoOfAllocatedLeave = (int) dr["NoOfLeaveAllocated"];
                totalLeave.Add(leave);
            }
            con.Close();
            return totalLeave.ToList();
        }

        public List<AllocatedLeave> AllCasualLeaveInfo(int employeeId)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(@"SELECT * FROM AllocatedLeave Where EmployeeId ='" + employeeId + "' and LeaveTypeId = '" + 2 + "' ", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<AllocatedLeave> totalLeave = new List<AllocatedLeave>();
            while (dr.Read())
            {
                AllocatedLeave leave = new AllocatedLeave();
                leave.Id = (int)dr["Id"];
                leave.NoOfAllocatedLeave = (int)dr["NoOfLeaveAllocated"];
                totalLeave.Add(leave);
            }
            con.Close();
            return totalLeave.ToList();
        }

        public List<AllocatedLeave> RemaingSickLeave(int employeeId)
        {
            SqlConnection con = new SqlConnection(connectionString);
          
            SqlCommand com = new SqlCommand(@"SELECT SUM(NoOfLeaveTaken) as number
FROM LeaveTaken
WHERE EmployeeId ='" + employeeId + "' and LeaveTypeId = '" + 1 + "'", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<AllocatedLeave> totalLeave = new List<AllocatedLeave>();
            while (dr.Read())
            {
                var AAA =   dr["number"];
                AllocatedLeave leave = new AllocatedLeave();
                if (!object.ReferenceEquals(dr["number"], DBNull.Value))
                {
                    leave.NoOfAllocatedLeave = Convert.ToInt32(dr["number"]);

                }
                else
                {
                    leave.NoOfAllocatedLeave = 0;
                }
              
                totalLeave.Add(leave);
            }
            con.Close();
            return totalLeave.ToList();
        }

        public List<AllocatedLeave> RemaingCasualLeave(int employeeId)
        {
            SqlConnection con = new SqlConnection(connectionString);
           
            SqlCommand com = new SqlCommand(@"SELECT SUM(NoOfLeaveTaken) as number
FROM LeaveTaken
WHERE EmployeeId ='" + employeeId + "' and LeaveTypeId = '" + 2 + "'", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<AllocatedLeave> totalLeave = new List<AllocatedLeave>();
            while (dr.Read())
            {
                AllocatedLeave leave = new AllocatedLeave();
                if (!object.ReferenceEquals(dr["number"], DBNull.Value))
                {
                    leave.NoOfAllocatedLeave = Convert.ToInt32(dr["number"]);
                    //exception
                }

                else
                {
                    leave.NoOfAllocatedLeave = 0;
                }
               
                totalLeave.Add(leave);
            }
            con.Close();
            return totalLeave.ToList();
        }

        public List<EmployeeLeaveReport> EmployeeLeaveList(int employeeId)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com =
                new SqlCommand(@"SELECT tb_Employees.Id, tb_Employees.Name, CONVERT(NVARCHAR,LeaveTaken.StartDate, 100) AS[StartDate],CONVERT(NVARCHAR,LeaveTaken.EndDate, 100) AS[EndDate],LeaveTaken.NoOfLeaveTaken, LeaveType.TypeName,CONVERT(NVARCHAR,LeaveTaken.EntryDate, 100) AS[EntryDate] 
FROM tb_Employees
INNER JOIN LeaveTaken ON tb_Employees.Id = LeaveTaken.EmployeeId
INNER JOIN LeaveType ON LeaveTaken.LeaveTypeId = LeaveType.Id Where tb_Employees.Id ='" + employeeId + "' ", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<EmployeeLeaveReport> totalLeaveList = new List<EmployeeLeaveReport>();
            while (dr.Read())
            {
                EmployeeLeaveReport leave = new EmployeeLeaveReport();

                leave.EmployeeId = (int)dr["Id"];
                leave.EmployeeName = dr["Name"].ToString();
                leave.EndDate =  dr["EndDate"].ToString();
                leave.EntryDate =  dr["EntryDate"].ToString();
                leave.LeaveType = dr["TypeName"].ToString();
                leave.NoOfLeaveTake =  dr["NoOfLeaveTaken"].ToString();
                leave.StartDate =  dr["StartDate"].ToString();

                totalLeaveList.Add(leave);
            }
            con.Close();
            return totalLeaveList.ToList();
        }
    }
}