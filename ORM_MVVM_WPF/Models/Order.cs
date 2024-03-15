using System;
using System.Collections.Generic;


namespace ORM_MVVM_WPF.Models
{
    public enum PaymentStatus
    {
        Pending,
        Paid
    }

    public enum OrderStatus
    {
        Pending,
        Approved,
        Shipped,
        Delivered,
        Cancelled
    }
    public class Order : IPayment
    {

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<Item> OrdersItemsByCustomer { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public Order()
        {
        }
        public bool ProcessPayment()
        {
            PaymentStatus = PaymentStatus.Paid;
            return true;
        }

    }
}
