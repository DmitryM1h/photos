using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.MiddleWare
{
    [Obsolete("чисто для практики")]
    public class HttpRequestLoggerMiddleWare(RequestDelegate _next,
                                        ILogger<HttpRequestLoggerMiddleWare> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Получен http запрос");
            _logger.LogInformation(context.Request.Path);
            _logger.LogInformation(context.Request.Method);
            _logger.LogInformation(context.Request.Protocol);
            _logger.LogInformation(context.Request.Host.Value);
            _logger.LogInformation(context.Connection.LocalIpAddress.ToString());
            _logger.LogInformation(context.Request.Cookies.ToString());

            await _next.Invoke(context);
        }

    }
}
