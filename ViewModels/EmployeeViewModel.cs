
using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CSharp.WPF.ADO.ConnectionModels.ViewModels
{
 
    public partial class EmployeeViewModel
    {

        #region EmployeeViewModel Properties

        private TextBox _tbFName;

        public TextBox TbFName
        {
            get { return _tbFName; }
            set { _tbFName = value; }
        }

        private TextBox _tbLName;

        public TextBox TbLName
        {
            get { return _tbLName; }
            set { _tbLName = value; }
        }

        public int EmployeeId
        {
            get { return userid; }
            set { userid = value; }
        }

        #endregion

        #region Private Members

        public ObservableCollection<Employee> EmployeeList { get; set; } = new ObservableCollection<Employee>();


        private string userfname;

       
        private string userlname;


        private int userid;


        #endregion

        #region Constructor

        public EmployeeViewModel()
        {
            _tbFName = new TextBox();

            _tbLName = new TextBox();


            LoadData();

            
        }

        #endregion

        #region Load Data Northwind Database

        public void LoadData()
        {

            // Employees from Northwind DB
            
            if (EmployeeList != null)
            {
                EmployeeList.Clear();
                DataServices.GetEmployeesAsync(EmployeeList);
            }
        }

        #endregion

        #region Initialize Listview
        public void InitializeUserInput(TextBox tbFName, TextBox tbLname)
        {
            _tbFName = tbFName;

            _tbLName = tbLname;


        }

        #endregion

        #region Helpers

        public void ClearUserInput()
        {
            _tbFName.Text = string.Empty;
            _tbLName.Text = string.Empty;
        }

        /// <summary>
        /// Reset ListView ItemsSource property, clear input fields
        /// and user id value
        /// </summary>
        public void Refresh_Page()
        {
            ClearUserInput();
            LoadData();
            userid = -1;
        }
        #endregion

        #region Relay Commands Employee

       
        public void SelectEmployee(int id)
        {

            EmployeeId = id;

            var query = from p in EmployeeList
                        where p.EmployeeId == EmployeeId
                        select p;

            foreach (var item in query)
            {
                _tbFName.Text = item.FirstName;
                _tbLName.Text = item.LastName;

            }

        }

   
        public async Task AddEmployee()
        {
            var fname = _tbFName.Text;
            var lname = _tbLName.Text;

            await DataServices.AddEmployee(fname, lname);
            Refresh_Page();
        }

        
        public async Task DeleteEmployee()
        {
            await DataServices.DeleteEmployee(EmployeeId);
            Refresh_Page();
        }

       
        public async Task EditEmployee()
        {
            var updatefname = _tbFName.Text;
            var updatelname = _tbLName.Text;
            await DataServices.EditEmployee(EmployeeId, updatefname, updatelname);
            Refresh_Page();

        }

        
        public void RefreshEmployee()
        {
            Refresh_Page();
        }
        #endregion

    }
}
