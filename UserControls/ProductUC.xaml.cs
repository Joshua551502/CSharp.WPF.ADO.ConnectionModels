using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.ViewModels;
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
    /// Interaction logic for ProductUC.xaml
    /// </summary>
    public partial class ProductUC : UserControl
    {
        ProductViewModel ViewModel = new ProductViewModel();
        public ProductUC()
        {
            InitializeComponent();

            ViewModel.InitializeUserInput(tbProdName, tbUnitsInStock);
            this.DataContext = ViewModel;
        }

        private void ProdItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product selectedProduct = (Product)button.DataContext;
            var productId = selectedProduct.ProductId;
            ViewModel.ProductId = productId;
            ViewModel.SelectProduct(productId);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddProduct();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EditProduct();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteProduct();
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Refresh_Page();
        }

    }
}
