using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Handler
{
    [TestClass]
    public class OrderHandlerTest
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _feeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderHandlerTest() 
        {
            _customerRepository = new FakeCustomerRepository();
            _feeRepository = new FakeDeliveryFeeRepository();
            _discountRepository = new FakeDiscountRepository();
            _orderRepository = new FakeOrderRepository();
            _productRepository = new FakeProductRepository();
        }
        
        
        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmClienteInexistenteOPedidoNaoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = null;
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Validate();
            

            Assert.AreEqual(command.Valid, false);

        }
        
        
        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmCepInvalidoOPedidoDeveSerGeradoNormalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.ZipCode = "";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            Assert.AreEqual(command.Valid, true);
        }
        
        
        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmPromocodeInexistenteOPedidoDeveSerGeradoNormalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.ZipCode = "13411080";
            command.PromoCode = "";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            Assert.AreEqual(command.Valid, true);
        }
        
        
        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmPedidoSemItensOMesmoNaoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            //command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            //command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();




            Assert.AreEqual(command.Valid, false);
        }


        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = null;
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }


        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoValidoOPedidoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(),1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository,
                _feeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(command);
            Assert.AreEqual(handler.Valid, true);
        }


    }
}
