using EmployeeDataModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDA
{
    public class EmployeeDA
    {
        private static string constr;
        private static ILogger<EmployeeDA> _logger;

       private static SqlConnection con = new SqlConnection();
       private static SqlCommand cmd = new SqlCommand();
       private static SqlDataReader sdr = null;
        
        public static List<Employee> GetAllEmployees()
        {
            List<Employee> list = new List<Employee>();
            constr = "Data Source=.;Initial Catalog=TestDB;Integrated Security=True";
            using (con = new SqlConnection(constr))
            {
                try
                {
                    cmd = new SqlCommand("[dbo].[GetEmployees]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        list.Add(new Employee
                        {
                            EmployeeID = Convert.ToInt32(sdr["EmployeeID"]),
                            FirstName = Convert.ToString(sdr["FirstName"]),
                            MiddleName = Convert.ToString(sdr["MiddleName"]),
                            LastName = Convert.ToString(sdr["LastName"])
                        });
                    }
                    sdr.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error at GetAllEmployees() : ");
                    list = null;
                }
            }
            return list;
        }

        public static Employee GetEmployee(int id)
        {
            Employee employee = new Employee();
            constr = "Data Source=.;Initial Catalog=TestDB;Integrated Security=True";
            using (con = new SqlConnection(constr))
            {
                try
                {
                    cmd = new SqlCommand("[dbo].[GetEmployeeByID]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        employee = new Employee
                        {
                            EmployeeID = Convert.ToInt32(sdr["EmployeeID"]),
                            FirstName = Convert.ToString(sdr["FirstName"]),
                            MiddleName = Convert.ToString(sdr["MiddleName"]),
                            LastName = Convert.ToString(sdr["LastName"])
                        };
                    }
                    sdr.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error at GetEmployee() : ");
                    employee = null;
                }
            }
            return employee;
        }


        public static string AddEmployee(Employee employee)
        {
            string result = "";
            constr = "Data Source=.;Initial Catalog=TestDB;Integrated Security=True";
            using (con = new SqlConnection(constr))
            {
                try
                {
                    cmd = new SqlCommand("[dbo].[InsertUpdateEmployee]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@pAction", "Save");
                    cmd.Parameters.Add("@pOut", SqlDbType.VarChar, 15);
                    cmd.Parameters["@pOut"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery(); 
                    result = Convert.ToString(cmd.Parameters["@pOut"].Value);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error at AddEmployee() : ");
                }
            }
            return result;
        }

        public static string UpdateEmployee(Employee employee)
        {
            string result = "";
            constr = "Data Source=.;Initial Catalog=TestDB;Integrated Security=True";
            using (con = new SqlConnection(constr))
            {
                try
                {
                    cmd = new SqlCommand("[dbo].[InsertUpdateEmployee]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@pAction", "update");
                    cmd.Parameters.Add("@pOut", SqlDbType.VarChar, 15);
                    cmd.Parameters["@pOut"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    result = Convert.ToString(cmd.Parameters["@pOut"].Value);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error at UpdateEmployee() : ");
                }
            }
            return result;
        }
        public static string DeleteEmployee(int id)
        {
            constr = "Data Source=.;Initial Catalog=TestDB;Integrated Security=True";
            string result = "";
            using (con = new SqlConnection(constr))
            {
                try
                {
                    cmd = new SqlCommand("[dbo].[InsertUpdateEmployee]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    cmd.Parameters.AddWithValue("@pAction", "delete");
                    cmd.Parameters.Add("@pOut", SqlDbType.VarChar, 15);
                    cmd.Parameters["@pOut"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToString(cmd.Parameters["@pOut"].Value);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error at DeleteEmployee() : ");
                }
                return result;
            }
        }
    }
}
