using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Test.Repositories;

namespace Store.Test.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryfreeRespository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderRepositoty _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryfreeRespository();
            _discountRepository = new FakeDiscountRepository();
            _orderRepository = new FakeOrderRepository();
            _productRepository = new FakeProductRepository();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreatOrderCommand();
            command.Customer = "123456789128";
            command.ZipCode = "123456789";
            command.PromoCode = "111111111";
            command.Items.Add(new CreatOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreatOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(command);

            Assert.AreEqual(handler.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreatOrderCommand();
            command.Customer = "123456789123";
            command.ZipCode = "123456789";
            command.PromoCode = "";
            command.Items.Add(new CreatOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreatOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(command);

            Assert.AreEqual(handler.IsValid, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado()
        {
            var command = new CreatOrderCommand();
            command.Customer = "123456789123";
            command.ZipCode = "123456789";
            command.PromoCode = "111111111";
            command.Items.Add(new CreatOrderItemCommand(Guid.Empty, 0));

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(command);

            Assert.AreEqual(handler.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreatOrderCommand();
            command.Customer = "";
            command.ZipCode = "987564321";
            command.PromoCode = "98786545";
            command.Items.Add(new CreatOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreatOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(command);

            Assert.AreEqual(command.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            var command = new CreatOrderCommand();
            command.Customer = "123456789123";
            command.ZipCode = "123456789";
            command.PromoCode = "111111111";
            command.Items.Add(new CreatOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreatOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(command);

            Assert.AreEqual(handler.IsValid, true);
        }
    }
}