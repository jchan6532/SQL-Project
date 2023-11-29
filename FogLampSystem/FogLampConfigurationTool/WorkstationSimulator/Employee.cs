using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationSimulator
{
    internal class Employee
    {
        public int EmployeeId
        {
            get;
            set;
        }

        public string EmployeeName
        {
            get
            {
                SqlConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT employee_name FROM Employee WHERE employee_id = {EmployeeId}",
                    conn);
                string employeeName = string.Empty;

                conn.Open();
                object response = cmd.ExecuteScalar();
                conn.Close();

                if (response != null)
                {
                    employeeName = (string)response;
                }

                return employeeName;
            }
        }

        public string EmployeeType
        {
            get
            {
                SqlConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT type_name FROM Employee " +
                                                $"INNER JOIN EmployeeType ON  Employee.employee_type = EmployeeType.type_id " +
                                                $"WHERE employee_id = {EmployeeId}",
                    conn);
                string employeeType = string.Empty;

                conn.Open();
                object response = cmd.ExecuteScalar();
                conn.Close();

                if (response != null)
                {
                    employeeType = (string)response;
                }

                return employeeType;
            }
        }

        public float BuildSpeedModifier
        {
            get
            {
                SqlConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT config_value FROM ConfigSettings WHERE config_key = 'employee.{EmployeeType}.build_speed'",
                    conn);
                float buildSpeed = 0.0f;

                conn.Open();
                object response = cmd.ExecuteScalar();
                conn.Close();

                if (response != null)
                {
                    buildSpeed = float.Parse((string)response);
                }

                return buildSpeed;
            }
        }

        public float DefectRateModifier
        {
            get
            {
                SqlConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT config_value FROM ConfigSettings WHERE config_key = 'employee.{EmployeeType.ToLower()}.defect_rate'",
                    conn);
                float defectRate = 0.0f;

                conn.Open();
                object response = cmd.ExecuteScalar();
                conn.Close();

                if (response != null)
                {
                    defectRate = float.Parse((string)response);
                }

                return defectRate;
            }
        }

        public Employee(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
