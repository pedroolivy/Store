namespace Store.Domain.Repositories
{
    public interface IDeliveryfreeRespository
    {
        decimal Get(string zipCode);
    }
}
