using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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

            var bytes = GetFormattedMessageBytes(msg);

            await response.Body.WriteAsync(bytes, 0, bytes.Length);
            await response.Body.FlushAsync();
        }

        private byte[] GetFormattedMessageBytes(string msg)
        {
            var messages = new StringBuilder();
            var lines = msg.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                messages.Append($"data:{line}\n");
            }
            messages.Append("\n\n");
            return Encoding.UTF8.GetBytes(messages.ToString(), 0, messages.Length);
        }
    }
}
