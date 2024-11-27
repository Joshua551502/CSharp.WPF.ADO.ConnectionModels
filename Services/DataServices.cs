
using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace CSharp.WPF.ADO.ConnectionModels.Services
{
    public static class DataServices
    {

        #region Connection String to MSSQLEXPRESS 
        /// <summary>
        /// Use Application Properties - Right Click app, select Properties and choose
        /// Settings - to entry strings for Application
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;";
        }


        #endregion

        #region Helpers
        public static async Task HandleException(Exception ex)
        {
            string msg = ex.Message.ToString();
            MessageBox.Show(msg);
            await Task.CompletedTask;
        }

        public static bool IsParameterEmpty(string fname, string lname)
        {
            var result = false;

            if (fname == "") result = true;

            if (lname == "") result = true;

            return result;
        }

        #endregion

        #region Async Task Retrieve Employees
        public async static void GetEmployeesAsync(ObservableCollection<Employee> employees)
        {

            try
            {

                var query = "Select * from Employees";

                var connection = new SqlConnection(GetConnectionString());
                connection.Open();

                var cmd = new SqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var _employee = new Employee
                    {
                        EmployeeId = reader.GetInt32(0)
                    };

                    if (!reader.IsDBNull(1))
                        _employee.FirstName = reader.GetString(2);

                    if (!reader.IsDBNull(2))
                        _employee.LastName = reader.GetString(1);

                    _employee.FullName = $"{_employee.FirstName} {_employee.LastName}";

                    employees.Add(_employee);
                }

                connection.Close();
            }
            catch (Exception ex)
            {

                await HandleException(ex);
                throw;

            }

        }

        #endregion

        #region Async Task Add Employee
        public static async Task AddEmployee(string fname, string lname)
        {
            if (IsParameterEmpty(fname, lname))
            {
                MessageBox.Show("No textbox can be empty");
                await Task.CompletedTask;
            }
            else
            {

                string insert = @"INSERT INTO [dbo].[Employees] ([LastName], [FirstName]) VALUES (@LastName, @FirstName); ";

                try
                {

                    var connection = new SqlConnection(GetConnectionString());
                    await connection.OpenAsync();

                    var cmd = new SqlCommand(insert, connection);

                    cmd.Parameters.AddWithValue("LastName", lname);
                    cmd.Parameters.AddWithValue("FirstName", fname);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {

                        MessageBox.Show($"Employee {fname} {lname} was inserted!");
                        await Task.CompletedTask;
                    }
                    else
                    {
                        MessageBox.Show($"Employee {fname} {lname} was not inserted!");
                        await Task.CompletedTask;
                    }

                    connection.Close();

                }
                catch (Exception ex)
                {
                    await HandleException(ex);
                }
            }
        }
        #endregion

        #region Async Task Edit Employee

        public static async Task EditEmployee(int empID, string fname, string lname)
        {
            if (IsParameterEmpty(fname, lname))
            {
                MessageBox.Show("No textbox can be empty");
                await Task.CompletedTask;
            }
            else
            {

                string update = @"UPDATE [dbo].[Employees] SET [LastName] = @LastName ,[FirstName] = @FirstName WHERE [EmployeeID]= @EmployeeID";

                try
                {

                    var connection = new SqlConnection(GetConnectionString());
                    await connection.OpenAsync();

                    var cmd = new SqlCommand(update, connection);

                    cmd.Parameters.AddWithValue("LastName", lname);
                    cmd.Parameters.AddWithValue("FirstName", fname);
                    cmd.Parameters.AddWithValue("EmployeeID", empID);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {

                        MessageBox.Show($"Employee {fname} {lname} was updated!");
                        await Task.CompletedTask;
                    }
                    else
                    {
                        MessageBox.Show($"Employee {fname} {lname} was not updated!");
                        await Task.CompletedTask;
                    }

                    connection.Close();

                }
                catch (Exception ex)
                {
                    await HandleException(ex);
                }

            }
        }
        #endregion

        #region Async Task Delete Employee
        public static async Task DeleteEmployee(int empID)
        {
            if(empID >= 12)
            {
                MessageBox.Show("Employee cannot be deleted from list!");
                await Task.CompletedTask;
            }
            else
            {
                string delete = @"DELETE FROM [dbo].[Employees] WHERE [EmployeeID]= @EmployeeID";

                try
                {

                    var connection = new SqlConnection(GetConnectionString());
                    await connection.OpenAsync();

                    var cmd = new SqlCommand(delete, connection);

                    cmd.Parameters.AddWithValue("EmployeeID", empID);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {

                        MessageBox.Show($"Employee was deleted!");
                        await Task.CompletedTask;
                    }
                    else
                    {
                        MessageBox.Show($"Employee was not deleted!");
                        await Task.CompletedTask;
                    }

                    connection.Close();

                }
                catch (Exception ex)
                {
                    await HandleException(ex);
                }
            }

        }
        #endregion

    }
}
