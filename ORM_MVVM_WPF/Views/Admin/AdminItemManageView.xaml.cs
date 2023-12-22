using ORM_MVVM_WPF.Models;
using ORM_MVVM_WPF.ViewModels.AdminViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ORM_MVVM_WPF.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminItemManageView.xaml
    /// </summary>
    public partial class AdminItemManageView : UserControl
    {
        public ObservableCollection<Models.Item> ItemsList { get; set; }
        public AdminItemManageView()
        {
            InitializeComponent();
            DataContext = this;
            DisplayItem_Click();         
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
           AddNewItemAdminViewWindow addNewItemAdminView = new AddNewItemAdminViewWindow();
            addNewItemAdminView.Show();
        }

        private void DisplayItem_Click()
        {
          AdminManageItemViewModel viewModel = new AdminManageItemViewModel();
            List<Models.Item> Items =viewModel.DisplayItem();
            ItemsList = new ObservableCollection<Models.Item>(Items);
        }


        private void AdditionalInfo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var ItemId = button.Tag;
                Models.Item selectedItemInfo = ItemsList.First(item => item.Id == Convert.ToInt32(button.Tag));
                string stg = "";
                if (selectedItemInfo is ItemElectronic electronic)
                {
                    stg += " Brand Name: "+Convert.ToString(electronic.Brand);
                }
                else if (selectedItemInfo is ItemCloth cloth)
                {
                    stg += " Size : " + Convert.ToString(cloth.Size) + "\n Material : " + cloth.Material;      
                }
                MessageBox.Show(stg);
            }
        }

        private void SearchItem_Click(object sender, RoutedEventArgs e)
        {
            //SearchItemName
            //SearchItemType
        }
    }
}
