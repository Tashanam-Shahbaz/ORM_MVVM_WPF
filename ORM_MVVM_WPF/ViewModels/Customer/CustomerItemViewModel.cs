using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_MVVM_WPF.Models;

namespace ORM_MVVM_WPF.ViewModels.Customer
{
    public class CustomerItemViewModel : BaseViewModel
    {
        private List<Item> itemsList;
        private ObservableCollection<Item> _itemObservableCollection;

        public CustomerItemViewModel()
        {
            BindItem();
        }
        public ObservableCollection<Item> ItemObservableCollection
        {
            get { return _itemObservableCollection; }
            set
            {
                _itemObservableCollection = value;
                CalculateSerialNumbers();
                OnPropertyChanged(nameof(ItemObservableCollection));
            }
        }

        private void BindItem()
        {
            itemsList = Serialization.DeSerializeList<Item>();
            ItemObservableCollection = new ObservableCollection<Item>(itemsList);
        }

        private void CalculateSerialNumbers()
        {
            int serialNumber = 1;
            foreach (var item in _itemObservableCollection)
            {
                item.SerialNumber = serialNumber++;
            }
        }
    }
}
