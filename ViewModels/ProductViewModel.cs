using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CSharp.WPF.ADO.ConnectionModels.ViewModels
{
    public class ProductViewModel
    {
        public CrudOperationsInDataSet crud = new CrudOperationsInDataSet();

        public ObservableCollection<Product> ProductList { get; set; } = new ObservableCollection<Product>();

        #region Private Members

        public TextBox TbProductName { get; set; }

        public TextBox tbUnitsInStock { get; set; }

        public int ProductId = 0;

        #endregion

        #region Constructor
        public ProductViewModel()
        {
            TbProductName = new TextBox();
            tbUnitsInStock = new TextBox();
            LoadData();
        }
        #endregion
        #region Load Data Northwind Database
        public void LoadData()
        {
            if (ProductList != null)
            {
                ProductList.Clear();
                crud.FillDataSet();
                crud.GetProducts(ProductList);
            }
        }
        #endregion

        #region SelectProduct
        public void SelectProduct(int id)
        {
            var row = crud.tblProducts.Rows.Find(id);
            TbProductName.Text = row[1].ToString();
            tbUnitsInStock.Text = row[2].ToString();
            ProductId = id;
        }
        #endregion

        #region Helpers
        public void ClearUserInput()
        {
            TbProductName.Text = string.Empty;
            tbUnitsInStock.Text = string.Empty;

        }
        /// <summary>
        /// Reset ListView ItemsSource property, clear input fields
        /// and user id value
        /// </summary>
        public void Refresh_Page()
        {
            ClearUserInput();
            LoadData();
        }
        #endregion

        #region Initalize User input TextBox
        public void InitializeUserInput(TextBox textBox, TextBox textBoxUnits)
        {
            TbProductName = textBox;
            tbUnitsInStock = textBoxUnits;
        }
        #endregion
        #region AddProduct
        public void AddProduct()
        {
            var productName = TbProductName.Text;
            short unitInStock = Convert.ToInt16(tbUnitsInStock.Text);

            crud.InsertProduct(productName, unitInStock);
            Refresh_Page();
        }
        #endregion

        #region EditProduct
        public async void EditProduct()
        {
            var updateproductname = TbProductName.Text;
            short updateunitinstock = Convert.ToInt16(tbUnitsInStock.Text);
            crud.EditProduct(ProductId, updateproductname, updateunitinstock);
            Refresh_Page();
        }
        #endregion

        #region DeleteProduct
        public void DeleteProduct()
        {
           
            crud.DeleteProduct(ProductId);
            Refresh_Page();
        }
        #endregion

        #region RefreshProduct
        public void RefreshProduct()
        {
            Refresh_Page();
        }
        #endregion

    }
}