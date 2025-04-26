using Store.Domain.Commands.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace Store.Domain.Commands
{
    public class CreatOrderCommand : Notifiable<Notification>, ICommand
    {
        public CreatOrderCommand() 
        { 
            Items = new List<CreatOrderItemCommand>();
        }

        public CreatOrderCommand(string customer, string zipCode, string promoCode, IList<CreatOrderItemCommand> items)
        {
            Customer = customer;
            ZipCode = zipCode;
            PromoCode = promoCode;
            Items = items;
        }

        public string Customer { get; set; }
        public string ZipCode { get; set; }
        public string PromoCode { get; set; }
        public IList<CreatOrderItemCommand> Items { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsGreaterThan(Customer, 11, "Customer", "Cliente inválido")
                    .IsGreaterThan(ZipCode, 8, "ZipCode", "Cep inválido")
            );
        }
    }
}