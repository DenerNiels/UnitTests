using System;
namespace Store.Store.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string title, decimal price, bool active)
        {
            Title = title;
            Price = price;
            Active = active;
        }

        //public Product(string title, decimal price, bool active)

        public string Title { get; private set; } 
        public decimal Price { get; private set; } 
        public bool Active { get; private set; } 

        //public void ChangeInfo(string title, decimal price, bool active)
        //{
        //    Title = title;
        //    Price = price;
        //    Active = active;
        //}
    }
}
