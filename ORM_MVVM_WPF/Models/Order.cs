using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Linq;
using System.Runtime.Serialization;
using ORM_MVVM_WPF.Utilities;

namespace ORM_MVVM_WPF.Models
{
    public enum PaymentStatus
    {
        All,
        Pending,
        Paid
    }

    public enum OrderStatus
    {
        All,
        Pending,
        Shipped,   // Seller will ship the order
        Delivered, //  admin will mark the order as delivered
        Completed, //  customer will mark the order as completed
        Cancelled //   admin or seller will cancel the order
    }
    public class Order : IPayment , INotifyPropertyChanged
    {
        private int _id;
        private int _customer_ID;
        private DateTime _orderDate;
        private List<int> _ordersItemIDIByCustomer;
        private List<Item> _ordersItemsByCustomer;
        private OrderStatus _orderStatus;
        private PaymentStatus _paymentStatus; 
        
        //Variables related to View only
        private int _serialNumber;
        private float _totalAmount;
        private bool _areAllItemsShipped;
        //private SerializableDictionary<string, OrderStatus> _orderStatusDictionary;
        private List<MyDictionary> _orderStatusDictionary;

        public Order()
        {
            OrderStatus = OrderStatus.Pending;
            PaymentStatus = PaymentStatus.Pending;
            //OrderStatusDictionary = new SerializableDictionary<string, OrderStatus>();
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

        public int Customer_Id
        {
            get { return _customer_ID; }
            set
            {
                if (_customer_ID != value)
                {
                    _customer_ID = value;
                    OnPropertyChanged(nameof(Customer_Id));
                }
            }
        }
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set
            {
                if (_orderDate != value)
                {
                    _orderDate = value;
                    OnPropertyChanged(nameof(OrderDate));
                }
            }
        }

        public List<int> OrdersItemIDByCustomer
        {
            get { return _ordersItemIDIByCustomer; }
            set
            {
                if (_ordersItemIDIByCustomer != value)
                {
                    _ordersItemIDIByCustomer = value;
                    OnPropertyChanged(nameof(OrdersItemIDByCustomer));
                }
            }
        }


        public OrderStatus OrderStatus
        {
            get { return _orderStatus; }
            set
            {
                if (_orderStatus != value)
                {
                    _orderStatus = value;
                    OnPropertyChanged(nameof(Order.OrderStatus));
                }
            }
        }
        public PaymentStatus PaymentStatus
        {
            get { return _paymentStatus; }
            set
            {
                if (_paymentStatus != value)
                {
                    _paymentStatus = value;
                    OnPropertyChanged(nameof(PaymentStatus));
                }
            }
        }
        [XmlIgnore]
        public List<Item> OrdersItemsByCustomer
        {
            get { return _ordersItemsByCustomer; }
            set
            {
                if (_ordersItemsByCustomer != value)
                {
                    _ordersItemsByCustomer = value;
                    OnPropertyChanged(nameof(OrdersItemsByCustomer));
                    CheckAllItemsShipped();
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

        [XmlIgnore]
        public float TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    OnPropertyChanged(nameof(TotalAmount));
                }
            }
        }

        [XmlIgnore]
        public bool AreAllItemsShipped
        {
            get { return _areAllItemsShipped; }
            set
            {
                if (_areAllItemsShipped != value)
                {
                    _areAllItemsShipped = value;
                    OnPropertyChanged(nameof(_areAllItemsShipped));
                }
            }
        }

        //public SerializableDictionary<string, OrderStatus> OrderStatusDictionary
        //{
        //    get { return _orderStatusDictionary; }
        //    set
        //    {
        //        if (_orderStatusDictionary != value)
        //        {
        //            _orderStatusDictionary = value;
        //            OnPropertyChanged(nameof(OrderStatusDictionary));
        //        }
        //    }
        //}

        public List<MyDictionary> OrderStatusDictionary
        {
            get { return _orderStatusDictionary; }
            set
            {
                if (_orderStatusDictionary != value)
                {
                    _orderStatusDictionary = value;
                    OnPropertyChanged(nameof(OrderStatusDictionary));
                }
            }
        }

        private void CheckAllItemsShipped()
        {
            if (OrdersItemsByCustomer.Count != 0)
                AreAllItemsShipped = OrdersItemsByCustomer.All(item => item.ItemStatusinOrder == OrderStatus.Shipped );
        }

        public bool ProcessPayment()
        {
            PaymentStatus = PaymentStatus.Paid;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
