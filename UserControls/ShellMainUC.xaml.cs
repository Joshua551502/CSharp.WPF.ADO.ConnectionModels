using System;
using System.Collections.Generic;
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
    /// Interaction logic for ShellMainUC.xaml
    /// </summary>
    public partial class ShellMainUC : UserControl
    {
        public ShellMainUC()
        {
            InitializeComponent();
        }

        #region Main Window Event Handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            EmplyeesBtn.IsChecked = true;
            FramePages.Source =  new Uri("/UserControls/EmployeeUC.xaml", UriKind.Relative);
        }

        private void CloseBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Page Navigation Buttons
        private void EmplyeesBtn_Click(object sender, RoutedEventArgs e)
        {

            FramePages.Source =  new Uri("/UserControls/EmployeeUC.xaml", UriKind.Relative);
        }

        private void ProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Source =  new Uri("/UserControls/ProductUC.xaml", UriKind.Relative);
        }

        private void CategoriesBtn_Click(object sender, RoutedEventArgs e)
        {

           // FramePages.Source =  new Uri("/UserControls/CategoryUC.xaml", UriKind.Relative);
        }


        #endregion

        private void ProductsBtn_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
