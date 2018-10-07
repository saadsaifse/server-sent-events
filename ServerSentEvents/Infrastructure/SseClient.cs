using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServerSentEvents.Infrastructure
{
    public class SseClient : ISseClient
    {
        readonly HttpResponse response;

        public Guid Id { get; private set; }

        public bool IsConnected { get; internal set; }

        public SseClient(Guid id, HttpResponse response)
        {
            Id = id;
            this.response = response ?? throw new ArgumentNullException(nameof(id));
            IsConnected = true;
        }

        public async Task SendAsync(string msg)
        {
            if (!IsConnected)
                throw new InvalidOperationException("Client is not connected!");

            var wireMessage = "data: " + msg + "\n\n";
            var bytes = Encoding.UTF8.GetBytes(wireMessage);
            await response.Body.WriteAsync(bytes, 0, bytes.Length);
            await response.Body.FlushAsync();
        }
    }
}
