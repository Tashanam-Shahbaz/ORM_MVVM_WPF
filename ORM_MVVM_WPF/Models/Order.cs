using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Xml.Serialization;

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
        shipped,   // Seller will ship the order
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
        private int _serialNumber;
        private float _totalAmount;

        public Order()
        {
            OrderStatus = OrderStatus.Pending;
            PaymentStatus = PaymentStatus.Pending;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
