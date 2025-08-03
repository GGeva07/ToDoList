namespace ToDoListAPI.Core.Domain.Common
{
    public class BaseEntity<TId> : AuditableEntity
    {
        public TId? Id { get; set; }
    }
}
