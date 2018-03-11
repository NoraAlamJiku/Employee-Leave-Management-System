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
    public class ValidationGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["LeaveManagenent"].ConnectionString;


        public bool IsLeaveTaken(LeaveTaken leaveTaken)
        {
           // string Query = "SELECT * FROM LeaveTaken WHERE (StartDate BETWEEN @StartDate AND @EndDate) AND (EndDate BETWEEN @StartDate AND @EndDate) AND (EmployeeId = @EmployeeId)";
            string Query = "SELECT * FROM LeaveTaken WHERE (StartDate <= @EndDate AND EndDate >= @StartDate AND EmployeeId = @EmployeeId)";
           SqlConnection Connection = new SqlConnection(connectionString);
           SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("StartDate", SqlDbType.Date);
            Command.Parameters["StartDate"].Value = leaveTaken.StartDate;
            Command.Parameters.Add("EndDate", SqlDbType.Date);
            Command.Parameters["EndDate"].Value = leaveTaken.EndDate;
            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = leaveTaken.EmployeeId;
           SqlDataReader Reader = Command.ExecuteReader();
            Reader.Read();
            bool isExist = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return isExist;
        }

    }
}