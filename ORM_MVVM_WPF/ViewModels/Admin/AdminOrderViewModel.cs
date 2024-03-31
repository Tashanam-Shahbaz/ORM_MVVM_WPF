using ORM_MVVM_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.ViewModels.Admin
{
    public class AdminOrderViewModel : BaseViewModel
    {
        private List<Order> orderList;
        private List<Item> itemList;
        private ObservableCollection<Order> _orderObservableCollection;
        private ObservableCollection<Item> _itemOCOrder;

        // Filters
        private PaymentStatus _paymentStatus;
        private OrderStatus _orderStatus;

        public AdminOrderViewModel()
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

        //Filters
        public PaymentStatus ComboPaymentStatus
        {
            get => _paymentStatus;
            set
            {
                if (_paymentStatus != value)
                {
                    _paymentStatus = value;
                    FilterOOC();
                    OnPropertyChanged(nameof(ComboPaymentStatus));

                }
            }
        }

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
        

        private void Bind()
        {
            OrderObservableCollection = new ObservableCollection<Order>();
            itemList = Serialization.DeSerializeList<Item>();
          

            orderList = Serialization.DeSerializeList<Order>();
            foreach (var order in orderList)
            {
                order.OrdersItemsByCustomer = itemList.Where(item =>
                                                    order.OrdersItemIDByCustomer.Contains(item.Id)).ToList();
                if (order.OrdersItemsByCustomer.Count > 0)
                {
                    order.TotalAmount = order.OrdersItemsByCustomer.Sum(item => item.Price);
                    OrderObservableCollection.Add(order);
                }

            }
            CalculateSerialNumbers();
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

        public bool DeliverOrder(int id)
        {
            try
            {
                var orderToUpdate = orderList.FirstOrDefault(or => or.Id == id);
                if (orderToUpdate != null)
                {
                    orderToUpdate.OrderStatus = OrderStatus.Delivered;
                    Serialization.SerializeList(orderList);
                    OrderObservableCollection = new ObservableCollection<Order>(orderList);
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