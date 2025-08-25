namespace ToDoListAPI.Core.Application.Memo
{
    public class CacheItem<T>
    {
        public T Value { get; set; }
        public DateTime Expiracion { get; set; }
    }

}
