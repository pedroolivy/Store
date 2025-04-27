using Flunt.Notifications;
using Store.Domain.Entities;
using Store.Domain.Commands;
using Store.Domain.Repositories;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Utils;

namespace Store.Domain.Handlers
{
    public class OrderHandler : Notifiable<Notification>, IHandler<CreatOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryfreeRespository _deliveryfreeRespository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepositoty _orderRepositoty;

        public OrderHandler(
            ICustomerRepository customerRepository, 
            IDeliveryfreeRespository deliveryfreeRespository, 
            IDiscountRepository discountRepository, 
            IProductRepository productRepository, 
            IOrderRepositoty orderRepositoty)
        {
            _customerRepository = customerRepository;
            _deliveryfreeRespository = deliveryfreeRespository;
            _discountRepository = discountRepository;
            _productRepository = productRepository;
            _orderRepositoty = orderRepositoty;
        }

        public ICommandResult Handle(CreatOrderCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Pedido inválido", command.Notifications);

            var customer = _customerRepository.Get(command.Customer);//Cliente
            var deliveryFee = _deliveryfreeRespository.Get(command.ZipCode);//Taxa de entrega
            var discount = _discountRepository.Get(command.PromoCode);//Desconto

            var products = _productRepository.Get(ExtraGuids.Extract(command.Items)).ToList();
            var order = new Order(customer, deliveryFee, discount);

            foreach (var item in command.Items) 
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            AddNotifications(order.Notifications);

            if (!order.IsValid)
                return new GenericCommandResult(false, "Falha ao gerar pedido", Notifications);

            _orderRepositoty.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso!", order);
        }
    }
}
