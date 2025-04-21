﻿using Store.Domain.Enums;

namespace Store.Domain.Entities
{
    public class Order : Entity
    {
        private IList<OrderItem> _items;

        public Order(Customer customer, decimal deliveryFee, Discount discount)
        {
            Customer = customer;
            Date = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8);
            DeliveryFee = deliveryFee;
            Discount = discount;
            Status = EOrderStatus.WaitingPayment;
            _items = new List<OrderItem>();
        }

        public Customer Customer { get; private set; }
        public DateTime Date { get; private set; }
        public string Number { get; private set; }
        public decimal DeliveryFee { get; private set; }
        public Discount Discount { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items { get { return _items.ToArray(); } }

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            _items.Add(item);
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach (var item in _items)
            {
                total += item.Total();
            }

            total += DeliveryFee;
            total -= Discount != null ? Discount.Value() : 0;

            return total;
        }
        public void Pay(decimal amount)
        {
            if (amount == Total())
                Status = EOrderStatus.WaitingDelivery;
        }

        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
        }
    }
}
