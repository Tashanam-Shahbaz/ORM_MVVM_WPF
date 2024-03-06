using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Animation;
using ORM_MVVM_WPF.Models;

namespace ORM_MVVM_WPF.ViewModels.AdminViewModel
{
    public class AdminManageItemViewModel : BaseViewModel
    {
        public ObservableCollection<Item> ItemObservableCollection = new ObservableCollection<Item>(); 
        public AdminManageItemViewModel()
        {            
        }
        public bool AddItem(string name,string description,int price ,int  size , string material)
        {
            List<Item> itemsList = Serialization.DeSerializeList<Item>();    
            
            ItemCloth cloth = new ItemCloth();

            if (itemsList.OfType<ItemCloth>().Any())
            {
                cloth.Id = itemsList.OfType<ItemCloth>().Max( item => item.Id ) + 1;
            }
            else 
            { 
            cloth.Id=1;
            }
            cloth.Name = name;
            cloth.Description = description;
            cloth.Size = size;
            cloth.Price = price;
            cloth.Material = material;
            itemsList.Add(cloth);
            ItemObservableCollection.Add(cloth);
            Serialization.SerializeList(itemsList);
            return true;
        }
        public bool AddItem(string name, string description, int price, Brand type)
        {
            List<Item> items = Serialization.DeSerializeList<Item>();
            

            ItemElectronic itemElectronic = new ItemElectronic();
            
            if (items.OfType<ItemElectronic>().Any())
            {
                itemElectronic.Id = items.OfType<ItemElectronic>().Max(item => item.Id) + 1;
            }
            else
            {
                itemElectronic.Id = 1;
            }

            itemElectronic.Name = name;
            itemElectronic.Description = description;
            itemElectronic.Brand = type;
            itemElectronic.Price = price;
            items.Add(itemElectronic);
            ItemObservableCollection.Add(itemElectronic);
            Serialization.SerializeList(items);
            return true;

        }

        public void DisplayItem()
        {
            List<Item> itemsList = Serialization.DeSerializeList<Item>();
            ItemObservableCollection = new ObservableCollection<Item>(itemsList);
        }
    }


}
