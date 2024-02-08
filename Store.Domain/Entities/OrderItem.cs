using Flunt.Validations;
using System.Diagnostics;

namespace Store.Store.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Product product, int quantity)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNull(product, "Product", "Produto inválido")
              .IsGreaterThan(quantity, 0, "Quantity", "A quantidade deve ser maior do que zero")
            );

            Product = product;
            Price = Product != null ? product.Price : 0;
            Quantity = quantity;
        }

        public Product Product { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public decimal Total()
        {
            return Price * Quantity;
        }
    }
}
