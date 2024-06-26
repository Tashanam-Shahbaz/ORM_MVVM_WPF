﻿using ORM_MVVM_WPF.Models;
using ORM_MVVM_WPF.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace ORM_MVVM_WPF.ViewModels.Customer
{
    public class CustomerOrderViewModel : BaseViewModel
    {
        private List<Order> orderList;
        private List<Models.Item> itemList;
        
        private ObservableCollection<Order> _orderObservableCollection;
        private ObservableCollection<Models.Item> _itemOCOrder;
        
        //For Filters
        private PaymentStatus _paymentStatus;
        private OrderStatus _orderStatus;

        private int cusId = ((Models.Customer)User.AuthUser).CustomerID;

        public CustomerOrderViewModel()
        {
            Bind();
        }
        public ObservableCollection<Models.Item> ItemOCOrder
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
            itemList = Serialization.DeSerializeList<Models.Item>();
            
            OrderObservableCollection = new ObservableCollection<Order>
                (
                orderList.Select(order =>
                {
                    order.OrdersItemsByCustomer = itemList
                    .Where(item => order.OrdersItemIDByCustomer.Contains(item.Id))
                    .ToList();

                    order.TotalAmount = order.OrdersItemsByCustomer.Sum(item => item.Price);
                    OrderStatus os;
                    
                    if (order.OrderStatusDictionary.TryGetValue($"cus_{cusId}", out os) 
                    || order.OrderStatusDictionary.TryGetValue("admin_0", out os))
                    {
                        order.OrderStatus = os;
                    }
                    else
                    {
                        // If neither 'cus_{cusId}' nor 'admin_0' entry exists, check other sellers for pending status
                        bool anyPending = order.OrderStatusDictionary.Values.Any(status => status == OrderStatus.Pending);
                        order.OrderStatus = anyPending ? OrderStatus.Pending : OrderStatus.Shipped;
                    }

                    return order;
                }
                    ).Where(order => order.Customer_Id == cusId && order.OrdersItemsByCustomer.Count > 0)
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
                (_orderStatus == OrderStatus.All || o.OrderStatus == _orderStatus) && o.Customer_Id == cusId;

            OrderObservableCollection = new ObservableCollection<Order>(orderList.Where(filterPredicate));
        }

        //Customer will place order.
        public bool PlaceOrder(object selectedItem)
        {
            try
            {
                Order order = new Order
                {
                    Id = orderList.Count + 1,
                    Customer_Id = ((Models.Customer)User.AuthUser).CustomerID,
                    OrderDate = DateTime.Now,
                    OrdersItemIDByCustomer = new HashSet<int>(),
                };

                HashSet<int> uniqueSellerIDs = new HashSet<int>();

                IEnumerable<Models.Item> enumerableSelectedOrders = (selectedItem as IEnumerable)?.OfType<Models.Item>();

                foreach (var item in enumerableSelectedOrders)
                {
                    order.OrdersItemIDByCustomer.Add(item.Id);
                    uniqueSellerIDs.Add(item.SellerID);
                }
                order.OrderStatusDictionary[$"sel_{cusId}"] = OrderStatus.Pending;
                foreach (int sellerID in uniqueSellerIDs)
                {
                    order.OrderStatusDictionary[$"sel_{sellerID}"] = OrderStatus.Pending;
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
            OrderObservableCollection = new ObservableCollection<Order>(OrderObservableCollection.Where(o => o.Customer_Id == cusId ));
        }

        public void PayOrder(int orderId)
        {

            var o = orderList.FirstOrDefault(or => or.Id == orderId);
            if (o != null)
            {
                o.PaymentStatus = PaymentStatus.Paid;
                Serialization.SerializeList(orderList);
                OrderObservableCollection = new ObservableCollection<Order>(orderList.Where(oi => oi.Customer_Id == cusId));

            }
            
        }
        public bool ConfirmDelivery(int orderId)
        {
            try
            {
                var o = orderList.FirstOrDefault(or => or.Id == orderId);
                if (o != null)
                {
                    o.OrderStatusDictionary[$"cus_{cusId}"] = OrderStatus.Completed;
                    o.OrderStatus = OrderStatus.Completed;
                    Serialization.SerializeList(orderList);
                    OrderObservableCollection = new ObservableCollection<Order>(orderList.Where(oi => oi.Customer_Id == cusId));
                }
                
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}