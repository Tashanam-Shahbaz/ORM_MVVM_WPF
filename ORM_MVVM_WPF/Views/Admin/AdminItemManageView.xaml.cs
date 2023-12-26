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
            List<Models.Item> Items = viewModel.DisplayItem();

            var distinctItemTypes = Items.Select(item => item.GetType()).Distinct().ToList();

            foreach (var itemType in distinctItemTypes)
            {
                var itemTypeList = Items.Where(item => item.GetType() == itemType).ToList();

                Type genericType = typeof(ObservableCollection<>).MakeGenericType(itemType);
                dynamic dynamicObservableCollection = Activator.CreateInstance(genericType);

                var addMethod = genericType.GetMethod("Add"); // Retrieve the 'Add' method

                foreach (var item in itemTypeList)
                {
                    addMethod.Invoke(dynamicObservableCollection, new[] { item }); // Invoke 'Add' method to add item
                }

                DataGrid dynamicDataGrid = new DataGrid();
                dynamicDataGrid.AutoGenerateColumns = true;
                dynamicDataGrid.ItemsSource = dynamicObservableCollection;
                DynamicDataGrid.Children.Add(dynamicDataGrid);
            }
        }

        private void SearchItem_Click(object sender, RoutedEventArgs e)
        {
            //SearchItemName
            //SearchItemType
        }
    }
}
