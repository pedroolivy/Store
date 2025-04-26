using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Test.Repositories
{//A ideia aqui é mostrar o conceito de Mocks, então esta sendo feito tudo manual........
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document == "123456789")
                return new Customer("Carlos", "teste@gmail.com");

            return null;
        }
    }
}
