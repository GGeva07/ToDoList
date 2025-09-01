namespace ToDoListAPI.Core.Application.Services.Cache
{
    public class CacheItem<T>
    {
        public T Value { get; set; }
        public DateTime Expiracion { get; set; }
    }

}
