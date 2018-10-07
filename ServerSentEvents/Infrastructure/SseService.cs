using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSentEvents.Infrastructure
{
    public class SseService : ISseService
    {
        readonly ConcurrentDictionary<Guid, SseClient> clients = new ConcurrentDictionary<Guid, SseClient>();

        public void AddClient(SseClient client)
        {
            clients.TryAdd(client.Id, client);
        }

        public IReadOnlyCollection<ISseClient> GetAllClients()
        {
            return clients.Values.ToArray();
        }

        public ISseClient GetClient(Guid id)
        {
            clients.TryGetValue(id, out SseClient client);
            return client;
        }

        public void RemoveClient(SseClient client)
        {
            clients[client.Id].IsConnected = false;
            clients.TryRemove(client.Id, out SseClient c);
        }

        public Task SendMessage(Guid clientId, string msg)
        {
            return clients[clientId].SendAsync(msg);
        }

        public Task SendToAll(string msg)
        {
            int index = 0;
            var tasks = new Task[clients.Keys.Count];
            foreach (var client in clients.Values)
            {
                if (client.IsConnected)
                {
                    tasks[index++] = client.SendAsync(msg);
                }
            }
            return Task.WhenAll(tasks);
        }
    }
}
