using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.Models
{

    public enum Brand
    {
        sony,
        samsung,
        lg
    }
    public class ItemElectronic : Item
    {
        public Brand Brand { get; set; }
        public override string Type => ItemType();

        public override string ItemType()
        {
            return "electronic";
        }
        public ItemElectronic()
        {

        }
    }
}
