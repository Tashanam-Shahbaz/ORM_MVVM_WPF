using System.Collections.Generic;
using System.Linq;
using ORM_MVVM_WPF.Models;

namespace ORM_MVVM_WPF.ViewModels.AdminViewModel
{
    public class AdminManageItemViewModel : BaseViewModel
    {
        public bool AddItem(string name,string description,int price ,int  size , string material)
        {
            List<Item> items = Serialization.DeSerializeList<Item>();    
            
            ItemCloth cloth = new ItemCloth();

            if (items.OfType<ItemCloth>().Any())
            {
                cloth.Id = items.OfType<ItemCloth>().Max( item => item.Id ) + 1;
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
            items.Add(cloth);
            Serialization.SerializeList(items);
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
            Serialization.SerializeList(items);
            return true;

        }

        public List<Item> DisplayItem()
        {
            List<Item> items = Serialization.DeSerializeList<Item>();
            return items;
        }
    }


}
