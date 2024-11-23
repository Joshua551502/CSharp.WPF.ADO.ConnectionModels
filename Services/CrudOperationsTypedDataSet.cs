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

        public string GetCategoryNameById(int id)
        {
            var row = _tbCategories.FindByCategoryID(id);

            if(row != null)
            {
                return row.CategoryName;
             
            }
            return String.Empty;
        }

        public void InsertCategory(string name)
        {
            if (IsParameterEmpty(name))
            {
                MessageBox.Show("Enter a name for Category!");
                return;
            }

            try
            {
                _adapter.Insert(name);
                MessageBox.Show($"Category {name} added successfully!");
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        public void EditCategory(int id, string name)
        {
            if (IsParameterEmpty(name))
            {
                MessageBox.Show("Enter a name for Category!");
                return;
            }

            try
            {
                var row = _tbCategories.FindByCategoryID(id);
                if (row != null)
                {
                    row.CategoryName = name;
                    _adapter.Update(_tbCategories);
                    MessageBox.Show($"Category {name} updated successfully!");
                }
            }
            catch (Exception ex)
            {
                Task.FromException(ex);
                throw;
            }
        }

        private bool IsParameterEmpty(string parameter)
        {
            return string.IsNullOrWhiteSpace(parameter);
        }

        public void DeleteCategory(int id)
        {
            if (id <= 0)
            {
                MessageBox.Show("Please select a valid category to delete!");
                return;
            }

            try
            {
                var name = GetCategoryNameById(id);

                if (!string.IsNullOrEmpty(name))
                {
                    _adapter.Delete(id, name);
                    MessageBox.Show($"Category {name} deleted successfully!");
                }
                else
                {
                    MessageBox.Show("The category does not exist.");
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
            if (_tbCategories != null && _tbCategories.Count > 0)
            {
                var row = _tbCategories[0];

                MessageBox.Show($"You selected {row.CategoryName} for deletion!");
            }
        }

        #endregion

    }
}
