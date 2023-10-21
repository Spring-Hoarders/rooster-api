using Microsoft.AspNetCore.Mvc;
using Rooster.Application.Building;
using Rooster.Common;

namespace Rooster.Building;

[Route("Buildings")]
public class GetBuildingsEndpoint : ApiControllerBase
{
    [HttpGet]
    public async Task<IList<BuildingResponse>> Get() => 
        await Mediator.Send(new GetBuildingsQuery());
}