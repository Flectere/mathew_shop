using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Page
    {
        public EditProduct()
        {
            InitializeComponent();
            PhotoProduct.Source = new BitmapImage(new Uri(App.selectedProduct.Photo, UriKind.Relative));
            NameTbx.Text = App.selectedProduct.Description;
            ProductTypeCbx.ItemsSource = App.db.Type.ToList();
            ProductTypeCbx.SelectedItem = App.selectedProduct.Type;
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

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductList());
        }

        private void editBTN_Click(object sender, RoutedEventArgs e)
        {
            if (NameTbx.Text.Length != 0)
            {
                App.selectedProduct.Description = NameTbx.Text;
                App.selectedProduct.Photo = "/photos/KL1.jpg";
                App.selectedProduct.Type = ProductTypeCbx.SelectedItem as Models.Type;
                MessageBox.Show("Данные изменены!");
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

        void textBox_PreviewTextDigitInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }
        }
    }
}