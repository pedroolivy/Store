﻿namespace Store.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string title, decimal price, bool active)
        {
            Title = title;
            Price = price;
            Active = active;
        }

        public Product(Guid id, string title, decimal price, bool active) 
            : this(title, price, active)
        {
            Id = id;
        }

        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public bool Active { get; private set; }
    }
}
