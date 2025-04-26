using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreatOrderItemCommand : Notifiable<Notification>, ICommand
    {
        public CreatOrderItemCommand() { }

        public CreatOrderItemCommand(Guid product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Guid Product { get; set; }
        public int Quantity { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsGreaterThan(Product.ToString(), 32, "Product", "Produto inválido")
                    .IsGreaterThan(Quantity, 0, "Quantity", "A quantidade inválida")
            );
        }
    }
}
