using CSharp.WPF.ADO.ConnectionModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSharp.WPF.ADO.ConnectionModels.Services
{
    public class CrudOperationsTypedDataSet
    {
        #region Private Members
        private NorthwindDataSetTableAdapters.CategoriesTableAdapter _adapter;
        private NorthwindDataSet.CategoriesDataTable _tbCategories;
        #endregion
        #region Constructor

        public CrudOperationsTypedDataSet()
        {
            _adapter = new NorthwindDataSetTableAdapters.CategoriesTableAdapter();
            _tbCategories = new NorthwindDataSet.CategoriesDataTable();
        }
        #endregion

        #region Methods Get, Add, Edit, Delete Categories
        public void GetAllCategories(ObservableCollection<Category> categories)
        {
            try
            {
                _tbCategories = _adapter.GetCategories();

                if (_tbCategories != null)
                {
                    foreach (var row in _tbCategories)
                    {
                        var category = new Category
                        {
                            CategoryId = row.CategoryID,
                            CategoryName = row.CategoryName,
                        };

                        categories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void GetCategoryByName(string name)
        {
            _tbCategories = _adapter.GetCategoryByName(name);
            if (_tbCategories.Count != null)
            {
                var row = _tbCategories[0];

                MessageBox.Show($"\nYou selected {row.CategoryName} for deletion!");
            }
        }

        #endregion

    }
}
