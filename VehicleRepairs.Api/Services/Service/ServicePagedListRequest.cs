namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Service.Models;

    public class ServicePagedListRequest : ServiceBaseRequest, IRequest<PagedList<ServiceViewModel>>
    {
    }
}
