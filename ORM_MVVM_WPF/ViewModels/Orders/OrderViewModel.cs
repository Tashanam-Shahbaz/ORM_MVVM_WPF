using ORM_MVVM_WPF.Models;
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
        private ObservableCollection<Order> _orderObservableCollection;

        public OrderViewModel()
        {
            BindOrder();
        }
        public ObservableCollection<Order> OrderObservableCollection
        {
            get { return _orderObservableCollection; }
            set
            {
                _orderObservableCollection = value;
                OnPropertyChanged(nameof(OrderObservableCollection));
            }
        }

        public bool PlaceOrder(object selectedItem)
        {
            try
            {
                Order order = new Order();
                if (orderList.Any())
                {
                    order.Id = orderList.Max(o => o.Id) + 1;
                }
                else
                {
                    order.Id = 1;
                }
                order.OrderDate = DateTime.Now;
                //order.customer_id = customer_id;
                order.OrdersItemsByCustomer = new List<Item>();

                IEnumerable<Item> enumerableSelectedOrders = (selectedItem as IEnumerable)?.OfType<Item>();
                foreach (var item in enumerableSelectedOrders.ToList())
                {
                    order.OrdersItemsByCustomer.Add(item);
                   
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
        private void BindOrder()
        {
            orderList = Serialization.DeSerializeList<Order>();
            OrderObservableCollection = new ObservableCollection<Order>(orderList);
        }
    }
}