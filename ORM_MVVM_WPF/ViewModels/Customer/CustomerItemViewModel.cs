using System;
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
        private ObservableCollection<Item> _itemOC;
        private List<Item> itemsList;
        public ObservableCollection<Item> ItemOC
        {
            get { return _itemOC; }
            set
            {
                _itemOC = value;
                OnPropertyChanged(nameof(ItemOC));
            }
        }
        public CustomerItemViewModel() 
        {
            BindItem();
        }

        public void BindItem()
        {
            itemsList = Serialization.DeSerializeList<Item>();
            _itemOC = new ObservableCollection<Item>(itemsList);
        }
    }
}
