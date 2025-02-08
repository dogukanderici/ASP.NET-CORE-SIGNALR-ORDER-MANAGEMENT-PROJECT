namespace SignalRWebUI.Areas.Admin.Models
{
    public class PagingDatasViewModel<TEntity>
    {
        public List<TEntity> Data { get; set; }
        public int Count { get; set; }
    }
}
