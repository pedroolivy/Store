using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface IOrderRepositoty
    {
        void Save(Order order);
    }
}
