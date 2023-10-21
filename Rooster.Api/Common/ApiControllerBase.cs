using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Rooster.Common;

public class ApiControllerBase : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}