using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.Models
{

    public enum ItemType
    {
            Cloth,
            Electronic
    }
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public virtual string Type => ItemType();
        public virtual string ItemType()
        {
            return "general";
        }

        public Item()
        {

        }

    }
}
