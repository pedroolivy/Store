using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Test.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>();
            int counter = 1;

            foreach (var id in ids)
            {
                products.Add(new Product(
                    id,
                    $"Produto {counter:00}",
                    10,
                    counter % 2 == 0                 
                ));

                counter++;
            }

            return products;
        }
    }
}
