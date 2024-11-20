
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

    public partial class ProductViewModel
    {

        #region ProductViewModel Properties

        private TextBox _tbProdName;

        public TextBox tbProdName
        {
            get { return _tbProdName; }
            set { _tbProdName = value; }
        }

        private TextBox _tbUnitsStock;

        public TextBox tbUnitsStock
        {
            get { return _tbUnitsStock; }
            set { _tbUnitsStock = value; }
        }

        public int ProductId
        {
            get { return productid; }
            set { productid = value; }
        }

        #endregion

        #region Private Members

        public ObservableCollection<Product> ProductList { get; set; } = new ObservableCollection<Product>();


        private string prodname;


        private string userlname;


        private int productid;


        #endregion

        #region Constructor

        public ProductViewModel()
        {
            _tbProdName = new TextBox();

            _tbUnitsStock = new TextBox();


            LoadData();


        }

        #endregion

        #region Load Data Northwind Database

        public void LoadData()
        {

            // Products from Northwind DB

            if (ProductList != null)
            {
                ProductList.Clear();
                DataServices.GetProductsAsync(ProductList);
            }
        }

        #endregion

        #region Initialize Listview
        public void InitializeUserInput(TextBox tbProdName, TextBox tbUnitsStock)
        {
            _tbProdName = tbProdName;

            _tbUnitsStock = tbUnitsStock;


        }

        #endregion

        #region Helpers

        public void ClearUserInput()
        {
            _tbProdName.Text = string.Empty;
            _tbUnitsStock.Text = string.Empty;
        }

        /// <summary>
        /// Reset ListView ItemsSource property, clear input fields
        /// and user id value
        /// </summary>
        public void Refresh_Page()
        {
            ClearUserInput();
            LoadData();
            productid = -1;
        }
        #endregion

        #region Relay Commands Product


        public void SelectProduct(int id)
        {

            ProductId = id;

            var query = from p in ProductList
                        where p.ProductId == ProductId
                        select p;

            foreach (var item in query)
            {
                _tbProdName.Text = item.FirstName;
                _tbUnitsStock.Text = item.LastName;

            }

        }


        public async Task AddProduct()
        {
            var fname = _tbProdName.Text;
            var lname = _tbUnitsStock.Text;

            await DataServices.AddProduct(fname, lname);
            Refresh_Page();
        }


        public async Task DeleteProduct()
        {
            await DataServices.DeleteProduct(ProductId);
            Refresh_Page();
        }


        public async Task EditProduct()
        {
            var updatefname = _tbProdName.Text;
            var updatelname = _tbUnitsStock.Text;
            await DataServices.EditProduct(ProductId, updatefname, updatelname);
            Refresh_Page();

        }


        public void RefreshProduct()
        {
            Refresh_Page();
        }
        #endregion

    }
}
