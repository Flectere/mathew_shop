using MathewShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MathewShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Page
    {
        int MaxPage = App.db.Product.Count() / 3;
        int FilterType = 2;

        public ProductList()
        {
            InitializeComponent();
            PageTb.Text = "1";
            int pageNumber = int.Parse(PageTb.Text);
            ProductLv.ItemsSource = App.db.Product.ToList().OrderBy(x => x.Description).Skip((pageNumber - 1) * 3).Take(3).ToList();
        }


        private void PageTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        private void FilterData()
        {
            if (PageTb.Text.Length == 0)
                return;
            int pageNumber = int.Parse(PageTb.Text);

            ProductLv.ItemsSource = App.db.Product.Where(x => x.Description.Contains(SearchTb.Text) && x.IdType==FilterType).OrderBy(x => x.Description).Skip((pageNumber - 1) * 3).Take(3).ToList();
        }


        private void ProductLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.selectedProduct = ProductLv.SelectedItem as Product;
            NavigationService.Navigate(new EditProduct());
        }
        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }
            if (int.Parse(PageTb.Text + e.Text) > MaxPage)
            {
                e.Handled = true;
                return;
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProduct());
        }

        private void CustomBtn_Click(object sender, RoutedEventArgs e)
        {
            FilterType = 2;
            FilterData();

        }

        private void CosplayBtn_Click(object sender, RoutedEventArgs e)
        {
            FilterType = 1;
            FilterData();

        }
    }
}