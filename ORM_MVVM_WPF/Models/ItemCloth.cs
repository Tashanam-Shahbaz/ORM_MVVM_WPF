using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.Models
{
    public class ItemCloth : Item
    {
        public int Size { get; set; }
        public string Material { get; set; }

        public override string Type => ItemType();

        public override string ItemType()
        {
            return "cloth";
        }

        public ItemCloth()
        {

        }
    }
}
