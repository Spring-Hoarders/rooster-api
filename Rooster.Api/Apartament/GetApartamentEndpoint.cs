using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rooster.Apartament;
using Rooster.Application.Building;
using Rooster.Common;
using System.Threading.Tasks;

namespace Rooster.Building
{
    [Route("Apartment")]
    public class ApartmentController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<ApartmentResponse>> Get(GetApartmentsByFloorQuery request)
            => await Mediator.Send(request);
    }
}
