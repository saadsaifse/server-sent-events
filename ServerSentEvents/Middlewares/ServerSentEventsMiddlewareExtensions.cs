using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSentEvents.Middlewares
{
    public static class ServerSentEventsMiddlewareExtensions
    {
        public static IApplicationBuilder UseServerSentEvents(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<ServerSentEventsMiddleware>();
        }

        public static IApplicationBuilder MapServerSentEvents(this IApplicationBuilder app, PathString path)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (!path.HasValue)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return app.Map(path, branchedApp => branchedApp.UseServerSentEvents());
        }
    }
}
