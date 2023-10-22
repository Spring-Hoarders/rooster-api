using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rooster.Apartament;
using Rooster.Application.Building;
using Rooster.Application.Clients;
using Rooster.Common;
using System.Threading.Tasks;

namespace Rooster.Building
{
    [Route("Client")]
    public class ClientController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<ClientResponse>> Get(GetClientsByApartmentIdQuery request)
            => await Mediator.Send(request);
    }
}
