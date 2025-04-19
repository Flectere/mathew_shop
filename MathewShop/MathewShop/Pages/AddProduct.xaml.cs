using MathewShop.Models;
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

namespace MathewShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {
        public AddProduct()
        {
            InitializeComponent();
            ProductTypeCbx.ItemsSource = App.db.Type.ToList();

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductList());

        }

        private void EditPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            openFileDialog.Filter = "Image Files | *.png; *.jpeg; *.jpg; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                PhotoProduct.Source = new BitmapImage(new Uri("/photos/KL1.jpg", UriKind.Relative));
                App.selectedProduct.Photo = "/photos/KL1.jpg";
                App.db.SaveChanges();
            }
        }

        private void editBTN_Click(object sender, RoutedEventArgs e)
        {
            if (NameTbx.Text.Length != 0 || ProductTypeCbx.SelectedItem != null)
            {
                Product product = new Product();
                product.Description = NameTbx.Text;
                product.Photo = "/photos/KL1.jpg";
                product.Type = ProductTypeCbx.SelectedItem as Models.Type;
                MessageBox.Show("Продукт добавлен");
                App.db.Product.Add(product);
                App.db.SaveChanges();
                NavigationService.Navigate(new ProductList());
            }
            else
            {
                MessageBox.Show("Введите данные!");
            }
        }
        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
