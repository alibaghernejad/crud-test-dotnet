using Marten;
using Mc2.CrudTest.Domain.Interfaces;

namespace Mc2.CrudTest.Infrastructure.EventSourcing;

public class MartenEventStore : IEventStore
{
    private readonly IDocumentStore _documentStore;

    public MartenEventStore(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task AppendEventAsync(Guid aggregateId, object @event)
    {
        using var session = _documentStore.LightweightSession();
        session.Events.Append(aggregateId, @event); // SQLite requires string ID
        await session.SaveChangesAsync();
    }
}