namespace zity.ExceptionHandling.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public string EntityName { get; }
        public object EntityId { get; }

        public ConcurrencyException(string entityName, object entityId)
            : base($"Concurrency conflict occurred for entity '{entityName}' with ID '{entityId}'.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }
}
