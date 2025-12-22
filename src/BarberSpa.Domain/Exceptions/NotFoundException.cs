namespace BarberSpa.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string entityName, object entityId)
            : base($"{entityName} with ID {entityId} not found.") { }
    }
}
