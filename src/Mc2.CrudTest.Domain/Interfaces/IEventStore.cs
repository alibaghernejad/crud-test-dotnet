namespace Mc2.CrudTest.Domain.Interfaces;

public interface IEventStore
{
    Task AppendEventAsync(Guid aggregateId, object @event);
}