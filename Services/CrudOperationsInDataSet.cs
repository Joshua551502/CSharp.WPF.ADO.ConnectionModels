using CSharp.WPF.ADO.ConnectionModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSharp.WPF.ADO.ConnectionModels.Services
{
    public class CrudOperationsInDataSet
    {
        #region Private Members

        private SqlConnection conn { get; set; }

        private SqlCommandBuilder cmdBuilder { get; set; }

        private SqlDataAdapter adapter { get; set; }

        private DataSet ds { get; set; }

        public DataTable tblProducts { get; set; }

        #endregion

        #region Helper
        public void FillDataSet()
        {
            ds = new DataSet();

            adapter.Fill(ds, "Products");

            tblProducts = ds.Tables["Products"];

            DataColumn[] pk = new DataColumn[1];

            pk[0] = tblProducts.Columns["ProductId"];

            tblProducts.PrimaryKey = pk;
        }
        #endregion
        public CrudOperationsInDataSet()
        {
            var cs = DataServices.GetConnectionString();
            var query = "Select ProductID, ProductName, UnitsInStock from Products";

            conn = new SqlConnection(cs);
            adapter = new SqlDataAdapter(query, conn);
            cmdBuilder = new SqlCommandBuilder(adapter);

            FillDataSet();
        }

        #region GetProducts
        public void GetProducts(ObservableCollection<Product> products)
        {
            try
            {
                if (tblProducts != null)
                {
                    foreach (DataRow row in tblProducts.Rows)
                    {
                        var _product = new Product
                        {
                            ProductId = (int)row["ProductID"],
                            ProductName = (string)row["ProductName"],
                            UnitInStock = (short)row["UnitsInStock"]
                        };
                        products.Add(_product);
                    }
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }
        #endregion

        #region InsertProduct
        public void InsertProduct(string name, short units)
        {
            if (IsParameterEmpty(name))
            {
                MessageBox.Show("Enter a name for product!");
                return;
            }

            try
            {
                DataRow newRow = tblProducts.NewRow();

                newRow["ProductID"] = 0;
                newRow["ProductName"] = name;
                newRow["UnitsInStock"] = units;

                tblProducts.Rows.Add(newRow);
                adapter.InsertCommand = cmdBuilder.GetInsertCommand();
                adapter.Update(tblProducts);

                FillDataSet();
                MessageBox.Show($"\nProduct {name} updated!");
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        private bool IsParameterEmpty(string name)
        {
            return string.IsNullOrWhiteSpace(name);
        }

        #endregion

        #region EditProduct
        public void EditProduct(int id, string name, short units)
        {
            if (IsParameterEmpty(name))
            {
                MessageBox.Show("Enter a name for product!");
                return;
            }

            try
            {
                // Find a row based on Primary Key
                DataRow row = tblProducts.Rows.Find(id);
                if (row != null)
                {
                    row["ProductName"] = name;
                    row["UnitsInStock"] = units;

                    adapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
                    adapter.Update(tblProducts);

                    FillDataSet();
                    MessageBox.Show($"\nProduct {name} updated!");
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }
        #endregion

        #region DeleteProduct
        public void DeleteProduct(int id = -1)
        {
            try
            {
                // Find a row based on Primary Key
                DataRow row = tblProducts.Rows.Find(id);
                if (row != null)
                {
                    var name = row["ProductName"];
                    row.Delete();

                    adapter.DeleteCommand = cmdBuilder.GetDeleteCommand();
                    adapter.Update(tblProducts);
                    FillDataSet();

                    MessageBox.Show($"\nProduct {name} deleted!");
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }
        #endregion


    }
}
