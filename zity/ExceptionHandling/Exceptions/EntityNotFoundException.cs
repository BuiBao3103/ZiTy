namespace zity.ExceptionHandling.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; }
        public object EntityId { get; }

        public EntityNotFoundException(string entityName, object entityId)
            : base($"Entity '{entityName}' with ID '{entityId}' was not found.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }
}
