using ORM_MVVM_WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.ViewModels.AdminViewModel
{
    public class AdminManageOrderViewModel : BaseViewModel
    {
        private List<Order> orderList;
        private List<Item> itemList;
        private ObservableCollection<Order> _orderObservableCollection;
        private ObservableCollection<Item> _itemOCOrder;

        private PaymentStatus _paymentStatus;
        private OrderStatus _orderStatus;

        public AdminManageOrderViewModel()
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

       
    }
}