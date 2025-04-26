using Store.Domain.Commands;

namespace Store.Test.Commands
{
    [TestClass]
    public class CreateOrderCommandTests
    {
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
            command.Validate();

            Assert.AreEqual(command.IsValid, false);
        }
    }
}
