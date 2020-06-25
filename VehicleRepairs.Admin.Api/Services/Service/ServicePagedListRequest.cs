namespace VehicleRepairs.Admin.Api.Services.Service
{
    using MediatR;
    using VehicleRepairs.Admin.Api.Services.Service.Models;
    using VehicleRepairs.Shared.Common;

    public class ServicePagedListRequest : ServiceBaseRequest, IRequest<PagedList<ServiceViewModel>>
    {
    }
}
