namespace Api.Extensions;

class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) { }
}