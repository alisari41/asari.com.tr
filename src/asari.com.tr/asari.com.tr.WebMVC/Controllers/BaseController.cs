using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebMVC.Controllers;

public class BaseController : Controller
{
    // Tamamen CQRS kullandığımız ve bunun içinde Mediator'ı kullandığımız için Mediator'a ihtiyaç olacak o yüzden BaseController yazıldı.
    protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    private IMediator? _mediator;
}
