using ORM_MVVM_WPF.Models;
using ORM_MVVM_WPF.ViewModels.AdminViewModel;
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
using System.Windows.Shapes;
using ORM_MVVM_WPF.ViewModels.Items;

namespace ORM_MVVM_WPF.Views.Admin
{
    /// <summary>
    /// Interaction logic for AddNewItemAdminViewWindow.xaml
    /// </summary>
    public partial class SellerAddItem : Window
    {
        ItemViewModel _viewModel;
        public SellerAddItem(ItemViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel ;
        }
        private void ItemTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemType type;
            Enum.TryParse(ItemTypeComboBox.SelectedItem.ToString(), out type);
            if (type == ItemType.Electronic)
            {
                BrandTypeComboBox.Visibility = Visibility.Visible;
                Brandlabel.Visibility = Visibility.Visible;
                SizeTextBox.Visibility = Visibility.Collapsed;
                SizeLabel.Visibility = Visibility.Collapsed;
                MaterialLabel.Visibility = Visibility.Collapsed;
                MaterialTextBox.Visibility = Visibility.Collapsed;
            }
            else if (type == ItemType.Cloth)
            {

                BrandTypeComboBox.Visibility = Visibility.Collapsed; 
                Brandlabel.Visibility = Visibility.Collapsed;
                SizeTextBox.Visibility = Visibility.Visible;
                SizeLabel.Visibility = Visibility.Visible;
                MaterialLabel.Visibility = Visibility.Visible;
                MaterialTextBox.Visibility = Visibility.Visible;
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                ItemTypeComboBox.SelectedItem == null)
            {
                ShowIncompleteFieldsMessage();
                return false;
            }

            ItemType selectedItemType;
            if (!Enum.TryParse(ItemTypeComboBox.SelectedItem?.ToString(), out selectedItemType))
            {
                return false;
            }

            switch (selectedItemType)
            {
                case ItemType.Cloth:
                    if (
                        string.IsNullOrWhiteSpace(SizeTextBox.Text) ||
                        string.IsNullOrWhiteSpace(MaterialTextBox.Text)
                        )
                    {
                        ShowIncompleteFieldsMessage();
                        return false;
                    }
                    break;

                case ItemType.Electronic:

                    if (BrandTypeComboBox.SelectedItem == null ) 
                    {
                        ShowIncompleteFieldsMessage();
                        return false;
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        private void ShowIncompleteFieldsMessage()
        {
            MessageBox.Show("Please fill in all the required fields.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void AddItem_Button(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                ItemType selectedItemType;
                Enum.TryParse(ItemTypeComboBox.SelectedItem?.ToString(), out selectedItemType);
                switch (selectedItemType)
                {
                    case ItemType.Cloth:
                        _viewModel.AddItem
                            (
                                 NameTextBox.Text,
                                 DescriptionTextBox.Text,
                                 Convert.ToInt32(PriceTextBox.Text),
                                 Convert.ToInt32(SizeTextBox.Text),
                                 MaterialTextBox.Text
                            );
                      
                        break;

                    case ItemType.Electronic:

                       Brand selectedElectronicType;
                        Enum.TryParse(BrandTypeComboBox.SelectedItem?.ToString(), out selectedElectronicType);
                        _viewModel.AddItem
                           (
                                NameTextBox.Text,
                                DescriptionTextBox.Text,
                                Convert.ToInt32(PriceTextBox.Text),
                               selectedElectronicType
                           );
                        break;
                    default:
                        break;
                }
                this.Close();
            }
        }
    }
}
