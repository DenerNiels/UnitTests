using Store.Store.Domain.Entities;
using Store.Store.Domain.Enums;
namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new Customer("Iaugo Florisanis", "iaugo@graficanf.com");
        private readonly Product _product = new Product("Produto 1", 10, true);
        private readonly Discount _discount10 = new Discount(10, DateTime.Now.AddDays(5));


        [TestMethod]
        [TestCategory("Domain")]
        public void DadoPedidoValidoDeveGerarNumeroCom8Caracteres()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoNovoPedidoSeuStatusDeveSerAguardandoPagamento()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoPagamentoNoPedidoSeuStatusDeveSerAguardandoEntrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1); // Total deve ser 10
            order.Pay(10);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoPedidoCanceladoSeuStatusDeveSerCancelado()
        {
            var order = new Order(_customer, 0, null);
            order.Cancel();
            Assert.AreEqual(order.Status, EOrderStatus.Canceled);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoItemSemProdutoNaoDeveSerAdicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(null, 10);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoNovoItemComQuantidadeZeroNaoDeveSerAdicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 0);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoNovoPedidoValidoSeuTotalDeveSer50()
        {
            var order = new Order(_customer, 10, _discount10);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoDescontoExpiradoValorDoPedidoDeveSer60()
        {
            var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5)); 
            var order = new Order(_customer, 10, expiredDiscount);        
            order.AddItem(_product, 5);                                     
            Assert.AreEqual(order.Total(), 60);                       
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoDescontoInvalidoNaoDeveSerAplicadoPedidoDeveSer60()
        {
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoDescontoValidoDe10ValorDoPedidoDeveSer50()
        {
            var order = new Order(_customer, 10, _discount10);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoTaxaDeEntrega10ValorDoPedidoDeveSer60()
        {
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoPedidoSemClienteDeveSerInvalido()
        {
            var order = new Order(null, 10, _discount10);
            Assert.AreEqual(order.Valid, false);
        }
    }
}
