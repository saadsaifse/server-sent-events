using Microsoft.AspNetCore.Http;
using ServerSentEvents.Infrastructure;
using System;
using System.Threading.Tasks;

namespace ServerSentEvents.Middlewares
{
    public class ServerSentEventsMiddleware
    {
        readonly ISseService serverSentEventsServie;
        readonly RequestDelegate next;

        public ServerSentEventsMiddleware(RequestDelegate next, ISseService sseService)
        {
            this.next = next;
            serverSentEventsServie = sseService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers["Accept"] ==  "text/event-stream")
            {
                context.Response.ContentType = "text/event-stream";

                var client = new SseClient(Guid.NewGuid(), context.Response);
                serverSentEventsServie.AddClient(client);

                await serverSentEventsServie.SendToAll("Client Connected");

                context.RequestAborted.WaitHandle.WaitOne();

                serverSentEventsServie.RemoveClient(client);

                await serverSentEventsServie.SendToAll("Client Disconnected");
            }
            else
            {
                await next(context);
            }
        }
    }
}
