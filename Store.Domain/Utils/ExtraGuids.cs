using Store.Domain.Commands;

namespace Store.Domain.Utils
{
    public static class ExtraGuids
    {
        public static IEnumerable<Guid> Extract(IList<CreatOrderItemCommand> items)
        {
            var guids = new List<Guid>();

            foreach (var item in items) 
                guids.Add(item.Product);

            return guids;
        }
    }
}
