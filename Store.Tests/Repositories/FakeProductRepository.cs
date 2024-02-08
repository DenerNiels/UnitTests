using Store.Domain.Repositories;
using Store.Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>
            {
                new Product("Produto 01", 10, true),
                new Product("Produto 02", 10, true),
                new Product("Produto 03", 10, true),
                new Product("Produto 04", 10, false),
                new Product("Produto 05", 10, false)
            };

            return products;
        }
    }
}
