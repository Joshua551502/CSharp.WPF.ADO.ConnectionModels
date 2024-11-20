using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region
    }
}
