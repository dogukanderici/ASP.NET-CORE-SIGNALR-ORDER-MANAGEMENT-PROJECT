namespace SignalRApi.Models
{
    public class PagingDataViewModel<TEntity>
    {
        public List<TEntity> Data { get; set; }
        public int Count { get; set; }
    }
}
