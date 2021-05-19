using MediatR;
using Microsoft.AspNetCore.Http;
using Scheduleservice.Services.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Common.Behaviors
{
    public class HttpContextMiddleware<TIn, TOut> : IPipelineBehavior<TIn, TOut>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextMiddleware(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                var hhaClaim = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "hha").FirstOrDefault().Value;
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "userId").FirstOrDefault().Value;
                if (request is BaseRequest br)
                {
                    br.UserId = Convert.ToInt32(userIdClaim);
                    br.HHA = Convert.ToInt32(hhaClaim);
                }
            }
            else
            {
                var hhaHeader = _httpContextAccessor.HttpContext.Request.Headers["hha"];
                var loginUserHeader = _httpContextAccessor.HttpContext.Request.Headers["user"];

                if (request is BaseRequest br)
                {
                    if(!String.IsNullOrEmpty(loginUserHeader))
                    br.UserId = Convert.ToInt32(loginUserHeader);
                    if (!String.IsNullOrEmpty(hhaHeader))
                        br.HHA = Convert.ToInt32(hhaHeader);
                } 
            }
            var result = await next();

            return result;
        }
    }
}
