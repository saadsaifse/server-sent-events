using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerSentEvents.Infrastructure
{
    public interface ISseService
    {
        IReadOnlyCollection<ISseClient> GetAllClients();
        ISseClient GetClient(Guid id);
        Task SendToAll(string msg);
        Task SendMessage(Guid clientId, string msg);
        void AddClient(SseClient client);
        void RemoveClient(SseClient client);
    }
}
