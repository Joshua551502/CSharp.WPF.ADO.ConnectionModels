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
    /// Interaction logic for ProductUC.xaml
    /// </summary>
    public partial class ProductUC : UserControl
    {
        ProductViewModel ViewModel = new ProductViewModel();
        public ObservableCollection<Product> ProductList { get; set; }

        public ProductUC()
        {
            InitializeComponent();
            ViewModel.InitializeUserInput(tbEmpFName, tbEmpLName);
            this.DataContext = ViewModel;
        }


        private void EmpItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product selectedProduct = (Product)button.DataContext;
            int ProductId = selectedProduct.ProductId;
            ViewModel.ProductId = ProductId;
            ViewModel.SelectProduct(ProductId);
        }

        private async void AddEmp_Click(object sender, RoutedEventArgs e)
        {
            //await ViewModel.AddProduct();
        }

        private void RefreshProducts_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }

        private async void EditEmp_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.EditProduct();
        }
    }
}
