using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CSharp.WPF.ADO.ConnectionModels.UserControls
{
    public partial class CategoryUC : UserControl
    {
        private readonly CategoryViewModel ViewModel = new CategoryViewModel();

        public CategoryUC()
        {
            InitializeComponent();
            ViewModel.InitializeUserInput(tbCatName);
            DataContext = ViewModel;
        }

        private async void AddCat_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AddCategory();
        }

        private async void EditCat_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.EditCategory();
        }

        private void RefreshCat_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshView();
        }

        private async void DeleteCat_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.DeleteCategory();
        }

        private void CatItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var selectedCategory = (Category)button.DataContext;
            ViewModel.SelectCategory(selectedCategory.CategoryId);
        }
    }
}
