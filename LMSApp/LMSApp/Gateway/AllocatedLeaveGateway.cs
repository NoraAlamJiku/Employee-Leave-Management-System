using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using LMSApp.Models;

namespace LMSApp.Gateway
{
    public class AllocatedLeaveGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["LeaveManagenent"].ConnectionString;
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

        public string AllocatedLeave(AllocatedLeave allocated)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(@"INSERT INTO [dbo].[AllocatedLeave]
           ([EmployeeId]
           ,[LeaveTypeId]
           ,[NoOfLeaveAllocated])
     VALUES
           ( '" + allocated.EmployeeId + "' , '" + allocated.LeaveTypeId + "' , '" + allocated.NoOfAllocatedLeave + "')", con);
            con.Open();
            int number = com.ExecuteNonQuery();
            con.Close();
            if (number > 0)
            {
                return "Employee Leave Allocation Save Successfully!";
            }
            return "Employee Leave Allocation Save Failed, Please Try Again!!!";
        }

        public bool IsLeaveAllocated(AllocatedLeave leaveTaken)
        {
            string Query = "SELECT * FROM AllocatedLeave WHERE (EmployeeId = @EmployeeId and LeaveTypeId = @LeaveTypeId)";
            SqlConnection Connection = new SqlConnection(connectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = leaveTaken.EmployeeId;
            Command.Parameters.Add("LeaveTypeId", SqlDbType.Int);
            Command.Parameters["LeaveTypeId"].Value = leaveTaken.LeaveTypeId;
            SqlDataReader Reader = Command.ExecuteReader();
            Reader.Read();
            bool isExist = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return isExist;
        }
    }
}