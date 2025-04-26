using Store.Domain.Repositories;

namespace Store.Test.Repositories
{//A ideia aqui é mostrar o conceito de Mocks, então esta sendo feito tudo manual........
    public class FakeDeliveryfreeRespository : IDeliveryfreeRespository
    {
        public decimal Get(string zipCode)
        {
            return 10;
        }
    }
}
