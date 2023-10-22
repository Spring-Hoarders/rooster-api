using MediatR;
using Rooster.Application.Clients;



namespace Rooster.Application.Clients
{
    public record GetClientsByApartmentIdQuery : IRequest <IList<ClientResponse>>
    {
        public Guid ApartmentId { get; set; }
    }

    public record ClientResponse
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string PhoneNumber { get; init; }
    }
}
