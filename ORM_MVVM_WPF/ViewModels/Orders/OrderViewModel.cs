﻿using ORM_MVVM_WPF.Models;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace ORM_MVVM_WPF.ViewModels.Orders
{
    public class OrderViewModel : BaseViewModel
    {
        private List<Order> orderList;
        private List<Item> itemList;
        private ObservableCollection<Order> _orderObservableCollection;
        private ObservableCollection<Item> _itemOCOrder; 
        
        private PaymentStatus _paymentStatus;
        private OrderStatus _orderStatus;

        public OrderViewModel()
        {
            Bind();
        }
        public ObservableCollection<Item> ItemOCOrder
        {
            get { return _itemOCOrder; }
            set
            {
                _itemOCOrder = value;
                FilterOOC();
                OnPropertyChanged(nameof(ItemOCOrder));
            }
        }

        public PaymentStatus ComboPaymentStatus
        {
            get => _paymentStatus;
            set
            {
                if (_paymentStatus != value)
                {
                    _paymentStatus = value;
                    OnPropertyChanged(nameof(ComboPaymentStatus));
                    FilterOOC();
                }
            }
        }

        //Customer Order Filter 
        public OrderStatus ComboOrderStatus
        {
            get => _orderStatus;
            set
            {
                if (_orderStatus != value)
                {
                    _orderStatus = value;
                    OnPropertyChanged(nameof(ComboOrderStatus));
                    FilterOOC();
                }
            }
        }

        public ObservableCollection<Order> OrderObservableCollection
        {
            get { return _orderObservableCollection; }
            set
            {
                _orderObservableCollection = value;
                CalculateSerialNumbers();
                OnPropertyChanged(nameof(OrderObservableCollection));
            }
        }

        private void Bind()
        {

            orderList = Serialization.DeSerializeList<Order>();
            itemList = Serialization.DeSerializeList<Item>();
            OrderObservableCollection = new ObservableCollection<Order>
                (
                orderList.Select(order =>
                    {
                        order.OrdersItemsByCustomer = itemList.Where(item => order.OrdersItemIDByCustomer.Contains(item.Id)).ToList();
                        order.TotalAmount = order.OrdersItemsByCustomer.Sum(item => item.Price);
                        return order;
                    }
                    )
                );
        }
        private void CalculateSerialNumbers()
        {
            int serialNumber = 1;
            foreach (var item in _orderObservableCollection)
            {
                item.SerialNumber = serialNumber++;
            }
        }
        private void FilterOOC()
        {
            Func<Order, bool> filterPredicate = o =>
                (_paymentStatus == PaymentStatus.All || o.PaymentStatus == _paymentStatus) &&
                (_orderStatus == OrderStatus.All || o.OrderStatus == _orderStatus);

            OrderObservableCollection = new ObservableCollection<Order>(orderList.Where(filterPredicate));
        }

        //Customer will place order.
        public bool PlaceOrder(object selectedItem)
        {
            try
            {
                Order order = new Order();
                order.Id = orderList.Count > 0 ?  orderList.Max(o => o.Id) + 1 : 1 ;
                order.Customer_Id = ((Models.Customer)User.AuthUser).CustomerID;
                order.OrderDate = DateTime.Now;
                order.OrdersItemIDByCustomer = new HashSet<int>();
                IEnumerable<Item> enumerableSelectedOrders = (selectedItem as IEnumerable)?.OfType<Item>();
                foreach (var item in enumerableSelectedOrders.ToList())
                {
                    order.OrdersItemIDByCustomer.Add(item.Id);

                }
                orderList.Add(order);
                OrderObservableCollection.Add(order);
                Serialization.SerializeList(orderList);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Customer can see his/her order
        public void ViewOrder()
        {
            OrderObservableCollection = new ObservableCollection<Order>(OrderObservableCollection.Where(o => o.Customer_Id == ((Models.Customer)User.AuthUser).CustomerID));
        }

        public void PayOrder(int orderId)
        {
           
           foreach (var order in orderList)
            {
                if (order.Id == orderId)
                {
                    order.PaymentStatus = PaymentStatus.Paid;
                    break;
                }
            }   
            Serialization.SerializeList(orderList);
            OrderObservableCollection = new ObservableCollection<Order>(orderList); 
        }
    }
}