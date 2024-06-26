﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ORM_MVVM_WPF.Models
{

    public enum ItemType
    {
            Cloth,
            Electronic
    }

    public enum ItemTypeDropDown
    {
        All,
        Cloth,
        Electronic,
    }

    public class Item : INotifyPropertyChanged
    {
        private int _id;
        private int _serialNumber; 
        private string _name;
        private string _description;
        private float _price;
        private int _seller_id;

        //Variables related to View only
        private OrderStatus _itemStatusinOrder;
        public Item()
        {
            ItemStatusinOrder = OrderStatus.Pending;
        }
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        [XmlIgnore]
        public int SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                if (_serialNumber != value)
                {
                    _serialNumber = value;
                    OnPropertyChanged(nameof(SerialNumber));
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        public float Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public int SellerID
        {
            get { return _seller_id; }
            set
            {
                if (_seller_id != value)
                {
                    _seller_id = value;
                    OnPropertyChanged(nameof(SellerID));
                }
            }
        }

        [XmlIgnore]
        public OrderStatus ItemStatusinOrder
        {
            get { return _itemStatusinOrder; }
            set
            {
                if (_itemStatusinOrder != value)
                {
                    _itemStatusinOrder = value;
                    OnPropertyChanged(nameof(_itemStatusinOrder));
                }
            }
        }


        public virtual string Type => ItemType();

       
        public virtual string ItemType()
        {
            return "general";
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
