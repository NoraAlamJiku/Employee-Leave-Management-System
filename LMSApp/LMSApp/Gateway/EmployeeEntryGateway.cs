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
    public class EmployeeEntryGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["LeaveManagenent"].ConnectionString;
        public List<Designation> DesignationList()
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(@"SELECT * FROM Designation", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<Designation> designationList = new List<Designation>();
            while (dr.Read())
            {
                Designation designation = new Designation();
                designation.Id = (int)dr["Id"];
                designation.Name = dr["Name"].ToString();
                designationList.Add(designation);
            }
            con.Close();
            return designationList.ToList();
        }

        public string SaveEmployee(Employee employee)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(@"INSERT INTO [dbo].[tb_Employees]
           ([Name]
           ,[Email]
           ,[DesignationId])
     VALUES
           ( '" + employee.Name + "' , '" + employee.Email + "' , '" + employee.DesignationId + "')", con);
            con.Open();
            int number = com.ExecuteNonQuery();
            con.Close();
            if (number > 0)
            {
                return "Employee Save Successfully!";
            }
            return "Employee Save Failed, Please Try Again!!!";
        }

        public bool IsEmailExist(string email)
        {
            bool isExists = false;

            SqlConnection Connection = new SqlConnection(connectionString);
            string Query = "SELECT Email FROM tb_Employees WHERE Email= @Email ";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();

            Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            Connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                isExists = true;
            }
            Connection.Close();
            Reader.Close();
            return isExists;
        }
    }
}