using System;
using System.Threading.Tasks;

namespace ServerSentEvents.Infrastructure
{
    public interface ISseClient
    {
        Guid Id { get; }

        bool IsConnected { get; }

        Task SendAsync(string msg);
    }
}
