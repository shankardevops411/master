using MediatR;
using Scheduleservice.Services.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.Common.Request
{
    public interface IRequestWrapper<T> : IRequest<Response<T>>
    {
    }

    public interface IHandlerWrapper<TIn, Tout> : IRequestHandler<TIn, Response<Tout>>
        where TIn : IRequestWrapper<Tout>
    {

    }

}
