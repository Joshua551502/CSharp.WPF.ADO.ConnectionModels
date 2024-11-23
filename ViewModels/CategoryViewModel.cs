using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CSharp.WPF.ADO.ConnectionModels.ViewModels
{
    public partial class CategoryViewModel
    {
        #region Properties
        private TextBox _tbCategoryName;

        public TextBox TbCategoryName
        {
            get { return _tbCategoryName; }
            set { _tbCategoryName = value; }
        }

        public int CategoryId { get; set; }

        public ObservableCollection<Category> CategoryList { get; set; } = new ObservableCollection<Category>();

        #endregion

        #region Private Members

        private readonly CrudOperationsTypedDataSet _crudService;

        #endregion

        #region Constructor

        public CategoryViewModel()
        {
            _tbCategoryName = new TextBox();
            _crudService = new CrudOperationsTypedDataSet();
            LoadCategories();
        }

        #endregion

        #region Load Data
        public void LoadCategories()
        {
            if (CategoryList != null)
            {
                CategoryList.Clear();
                _crudService.GetAllCategories(CategoryList);
            }
        }
        #endregion

        #region Initialize Input
        public void InitializeUserInput(TextBox tbCategoryName)
        {
            _tbCategoryName = tbCategoryName;
        }
        #endregion

        #region Helpers
        public void ClearUserInput()
        {
            _tbCategoryName.Text = string.Empty;
        }

        public void RefreshView()
        {
            ClearUserInput();
            LoadCategories();
            CategoryId = -1;
        }
        #endregion

        #region Relay Commands

        public void SelectCategory(int id)
        {
            CategoryId = id;

            var selectedCategory = CategoryList.FirstOrDefault(c => c.CategoryId == id);
            if (selectedCategory != null)
            {
                _tbCategoryName.Text = selectedCategory.CategoryName;
            }
        }

        public async Task AddCategory()
        {
            var categoryName = _tbCategoryName.Text;

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                _crudService.InsertCategory(categoryName);
                RefreshView();
            }
            else
            {
                await Task.FromException(new Exception("Category name cannot be empty."));
            }
        }

        public async Task EditCategory()
        {
            var categoryName = _tbCategoryName.Text;

            if (!string.IsNullOrWhiteSpace(categoryName) && CategoryId > 0)
            {
                _crudService.EditCategory(CategoryId, categoryName);
                RefreshView();
            }
            else
            {
                await Task.FromException(new Exception("Category name cannot be empty or invalid selection."));
            }
        }

        public async Task DeleteCategory()
        {
            if (CategoryId > 0)
            {
                _crudService.DeleteCategory(CategoryId);
                RefreshView();
            }
            else
            {
                await Task.FromException(new Exception("Invalid category selection."));
            }
        }

        public async Task<string> GetCategoryNameById()
        {
            if (CategoryId > 0)
            {
                return _crudService.GetCategoryNameById(CategoryId);
            }

            await Task.FromException(new Exception("Invalid category ID."));
            return string.Empty;
        }

        #endregion
    }
}
