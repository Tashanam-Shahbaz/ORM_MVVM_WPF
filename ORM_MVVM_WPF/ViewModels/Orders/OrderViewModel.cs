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
        private List<Item> itemList;
        private ObservableCollection<Order> _orderObservableCollection;
        private ObservableCollection<Item> _itemOCOrder;  

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
                OnPropertyChanged(nameof(ItemOCOrder));
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
            //int count = 0; 
            OrderObservableCollection = new ObservableCollection<Order>
                (
                orderList.Select(order =>
                    {
                        //count += 1;
                        //order.SerialNumber = count ;
                        order.OrdersItemsByCustomer = itemList.Where(item => order.OrdersItemIDByCustomer.Contains(item.Id)).ToList();
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

        //Customer will place order.
        public bool PlaceOrder(object selectedItem)
        {
            try
            {
                Order order = new Order();
                order.Id = orderList.Count > 0 ?  orderList.Max(o => o.Id) + 1 : 1 ;
                order.Customer_Id = ((Models.Customer)User.AuthUser).CustomerID;
                order.OrderDate = DateTime.Now;
                order.OrdersItemIDByCustomer = new List<int>();
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
    }
}