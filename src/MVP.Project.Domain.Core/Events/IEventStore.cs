using NetDevPack.Messaging;

namespace MVP.Project.Domain.Core.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}