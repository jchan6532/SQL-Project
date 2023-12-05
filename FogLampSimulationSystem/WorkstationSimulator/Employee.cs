/*
 * FILE : Employee.cs
 * PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
 * PROGRAMMER : Gerritt Hooyer, Justin Chan
 * FIRST VERSION : 2023-11-20
 * DESCRIPTION :
 * Runs the simulator in Runner or Workstation mode.
 */

using System.Configuration;
using System.Data.SqlClient;

namespace WorkstationSimulator
{
    /// <summary>
    /// Model class for representing one row entry in the database Employee table
    /// </summary>
    internal class Employee
    {
        #region Public Properties

        /// <summary>
        /// The employee ID for the current employee
        /// </summary>
        public int EmployeeId
        {
            get;
            set;
        }

        /// <summary>
        /// The employee name for the current employee
        /// </summary>
        public string EmployeeName
        {
            get
            {
                SqlConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT employee_name FROM Employee WHERE employee_id = {EmployeeId}",
                    conn);
                string employeeName = string.Empty;

                conn.Open();
                object response = cmd.ExecuteScalar();
                conn.Close();

                if (response != null)
                {
                    employeeName = response.ToString();
                }

                return employeeName;
            }
        }

        /// <summary>
        /// The employeeType for the current employee
        /// </summary>
        public string EmployeeType
        {
            get
            {
                SqlConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
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
                    employeeType = response.ToString();
                }

                return employeeType;
            }
        }

        /// <summary>
        /// The employee build speed multiplier/modifier based on the employee type for the current employee
        /// </summary>
        public float BuildSpeedModifier
        {
            get
            {
                SqlConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT config_value FROM ConfigSettings WHERE config_key = 'employee.{EmployeeType}.build_speed'",
                    conn);
                float buildSpeed = 0.0f;

                conn.Open();
                object response = cmd.ExecuteScalar();
                conn.Close();

                if (response != null)
                {
                    float.TryParse((string)response, out buildSpeed);
                }

                return buildSpeed;
            }
        }

        /// <summary>
        /// The employee defect rate modifier based on the employee type for the current employee
        /// </summary>
        public float DefectRateModifier
        {
            get
            {
                SqlConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT config_value FROM ConfigSettings WHERE config_key = 'employee.{EmployeeType.ToLower()}.defect_rate'",
                    conn);
                float defectRate = 0.0f;

                conn.Open();
                object response = cmd.ExecuteScalar();
                conn.Close();

                if (response != null)
                {
                    float.TryParse((string)response, out defectRate);
                }

                return defectRate;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized Constructor that takes an employee ID and instantiates a new employee object
        /// </summary>
        /// <param name="employeeId"></param>
        public Employee(int employeeId)
        {
            EmployeeId = employeeId;
        }

        #endregion
    }
}
