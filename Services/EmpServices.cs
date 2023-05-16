using SP_SQLClientCRUD.Models;
using System.Data;
using System.Data.SqlClient;

namespace SP_SQLClientCRUD.Services
{
    public class EmpServices : IEmpServices 
    {
        string connectionString = string.Empty;

        private readonly IConfiguration configuration;

        public EmpServices(IConfiguration _configuration) 
        {
            connectionString = _configuration.GetConnectionString("Myconnection");
        }




        //Get all Employee
        public IEnumerable<EmpModel> GetAllEmployees() 
        { 
            List<EmpModel> allemployees = new List<EmpModel>();
            using (SqlConnection cnct = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("allemp", cnct);
                cmd.CommandType = CommandType.StoredProcedure;
                cnct.Open();
                SqlDataReader rder = cmd.ExecuteReader();

                while (rder.Read()) 
                {
                    EmpModel emp = new EmpModel();
                    emp.emp_id = Convert.ToInt32(rder["emp_id"]);
                    emp.emp_name = Convert.ToString(rder["emp_name"]);
                    emp.emp_designation = Convert.ToString(rder["emp_designation"]);
                    emp.emp_salary = Convert.ToInt32(rder["emp_salary"]);

                    allemployees.Add(emp);
                }
                cnct.Close();
            }
            return allemployees;
        }




        //Add an Employee and return the ID of the last entered Employee
        public int AddEmployee(EmpModel emp)
        {
            int empid = 0;
            using (SqlConnection cnct = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("addemp", cnct);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@empname", emp.emp_name);
                cmd.Parameters.AddWithValue("@empdesignation", emp.emp_designation);
                cmd.Parameters.AddWithValue("@empsalary", emp.emp_salary);

                cnct.Open();
                SqlDataReader rder = cmd.ExecuteReader();
                
                while (rder.Read())
                {
                    empid = Convert.ToInt32(rder["empid"]);
                }
                cnct.Close();
            }
            return empid;            
        }




        //Edit an Employee
        public int UpdateEmployee(EmpModel emp)
        {
            using (SqlConnection cnct = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("updateemp", cnct);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@empid", emp.emp_id);
                cmd.Parameters.AddWithValue("@empname", emp.emp_name);
                cmd.Parameters.AddWithValue("@empdesignation", emp.emp_designation);
                cmd.Parameters.AddWithValue("@empsalary", emp.emp_salary);

                cnct.Open();
                cmd.ExecuteReader();
                cnct.Close();
            }
            return 0;
        }




        //Read Employee by ID
        public EmpModel Readbyid(int emp)
        {
            int empid = 0;
            EmpModel reslt = null;
            using (SqlConnection cnct = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("empbyid", cnct);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@empid", emp);

                cnct.Open();
                SqlDataReader rder = cmd.ExecuteReader();
                while (rder.Read())
                {
                    EmpModel emp1 = new EmpModel();

                    emp1.emp_id = Convert.ToInt32(rder["emp_id"]);
                    emp1.emp_name = Convert.ToString(rder["emp_name"]);
                    emp1.emp_designation = Convert.ToString(rder["emp_designation"]);
                    emp1.emp_salary = Convert.ToInt32(rder["emp_salary"]);

                    reslt = emp1;
                }
                cnct.Close();
            }
            return reslt;
        }




        //Delete an Employee
        public int DeleteEmployee(int empid) 
        { 
            using SqlConnection cnct = new SqlConnection(connectionString);
            {
                SqlCommand cmd = new SqlCommand("deleteemp",cnct);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@empid", empid);
                cnct.Open();
                cmd.ExecuteNonQuery();
                cnct.Close();
            }
            return 0;
        }
    }
}
