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

        private void BindOrder()
        {
            orderList = Serialization.DeSerializeList<Order>();
            OrderObservableCollection = new ObservableCollection<Order>(orderList);
        }

        //Customer will place order.
        public bool PlaceOrder(object selectedItem)
        {
            try
            {
                Order order = new Order();
                order.Customer_Id = ((Models.Customer)User.AuthUser).CustomerID;
                order.OrderDate = DateTime.Now;
                order.OrdersItemsByCustomer = new List<Item>();

                if (orderList.Any())
                {
                    order.Id = orderList.Max(o => o.Id) + 1;
                }
                else
                {
                    order.Id = 1;
                }

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

        //Customer can see his/her order
        public void ViewOrder()
        {
            OrderObservableCollection = new ObservableCollection<Order>(OrderObservableCollection.Where(o => o.Customer_Id == ((Models.Customer)User.AuthUser).CustomerID));
        }
    }
}