using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ORM_MVVM_WPF.Utilities;
using ORM_MVVM_WPF.ViewModels;

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
    public class Order : BaseViewModel , IPayment
    {
        private int _id;
        private int _customer_ID;
        private DateTime _orderDate;
        private HashSet<int> _ordersItemIDIByCustomer;
        private List<Item> _ordersItemsByCustomer;
        private SerializableDictionary<string, OrderStatus> _orderStatusDictionary;
        
        //Variables related to View only
        private int _serialNumber;
        private float _totalAmount;
        private OrderStatus _orderStatus;
        private PaymentStatus _paymentStatus;

        //private List<MyDictionary> _orderStatusDictionary;

        public Order()
        {
            OrderStatus = OrderStatus.Pending;
            PaymentStatus = PaymentStatus.Pending;
            OrderStatusDictionary = new SerializableDictionary<string, OrderStatus>();
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

        public HashSet<int> OrdersItemIDByCustomer
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

        public SerializableDictionary<string, OrderStatus> OrderStatusDictionary
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

        [XmlIgnore]
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


        public bool ProcessPayment()
        {
            PaymentStatus = PaymentStatus.Paid;
            return true;
        }

    }
}
