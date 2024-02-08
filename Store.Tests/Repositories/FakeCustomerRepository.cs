using Store.Domain.Repositories;
using Store.Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document == "12345678910")
                return new Customer("Iaugo Floris Anis", "iaugo@graficanf.com");

            return null;
        }
    }
}
