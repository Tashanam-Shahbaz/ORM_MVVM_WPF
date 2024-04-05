using ORM_MVVM_WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.ViewModels.Seller
{
    public class SellerOrderViewModel : BaseViewModel
    {
        private List<Order> orderList;
        private List<Item> itemList;
        private List<Item> filterSellerItemsList;
        
        private ObservableCollection<Order> _orderObservableCollection;
        private ObservableCollection<Item> _itemOCOrder;

        //Variables related to Filter
        private PaymentStatus _paymentStatus;
        private OrderStatus _orderStatus;

        private int  sellerId = ((Models.Seller)User.AuthUser).SellerID;

        public SellerOrderViewModel()
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
            OrderObservableCollection = new ObservableCollection<Order>();
            itemList = Serialization.DeSerializeList<Item>();
            filterSellerItemsList = itemList.Where(i => i.SellerID == sellerId).ToList();
            
            if (filterSellerItemsList.Count == 0)
                return; // No items to show
            
            orderList = Serialization.DeSerializeList<Order>();
            int serialNumber = 1;
            foreach (var order in orderList)
            {
                order.OrdersItemsByCustomer = filterSellerItemsList
                                               .Where(item => order.OrdersItemIDByCustomer.Contains(item.Id))
                                               .ToList();

                if (order.OrdersItemsByCustomer.Count > 0)
                {
                    order.SerialNumber = serialNumber++ ;
                    order.TotalAmount = order.OrdersItemsByCustomer.Sum(item => item.Price);
                    if (order.OrderStatusDictionary.TryGetValue($"sel_{sellerId}", out OrderStatus orderStatus))
                    {
                        order.OrderStatus = orderStatus;
                    } 
                    OrderObservableCollection.Add(order);
                }
            
            }
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
                (_orderStatus == OrderStatus.All || o.OrderStatus == _orderStatus) &&
                o.OrdersItemsByCustomer.Any(i => i.SellerID == sellerId);


            OrderObservableCollection = new ObservableCollection<Order>(orderList.Where(filterPredicate));
        }

        public bool ShipItem(int id)
        {
            try
            {
                Order orderToUpdate = orderList.FirstOrDefault(or => or.Id == id);
                orderToUpdate.OrderStatusDictionary[$"sel_{sellerId}"] = OrderStatus.Shipped;
                orderToUpdate.OrderStatus = OrderStatus.Shipped;
                Serialization.SerializeList(orderList);
                OrderObservableCollection = new ObservableCollection<Order>(orderList);
            }
            catch 
            { 
                return false;
            }
            return true;
        }
    }
}