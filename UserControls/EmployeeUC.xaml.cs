using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.Services;
using CSharp.WPF.ADO.ConnectionModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharp.WPF.ADO.ConnectionModels.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeeUC.xaml
    /// </summary>
    public partial class EmployeeUC : UserControl
    {
        EmployeeViewModel ViewModel = new EmployeeViewModel();
        public ObservableCollection<Employee> EmployeeList { get; set; }

        public EmployeeUC()
        {
            InitializeComponent();
            ViewModel.InitializeUserInput(tbEmpFName, tbEmpLName);
            this.DataContext = ViewModel;
        }


        private void EmpItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Employee selectedEmployee = (Employee)button.DataContext;
            int employeeId = selectedEmployee.EmployeeId;
            ViewModel.EmployeeId = employeeId;
            ViewModel.SelectEmployee(employeeId);
        }

        private async void AddEmp_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AddEmployee();
        }

        private void RefreshEmployees_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }

        private async void EditEmp_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.EditEmployee();

        }private async void DeleteEmp_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.DeleteEmployee();
        }
    }
}
