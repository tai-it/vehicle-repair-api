namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Ordering.Models;

    public class GetMineOrderRequest : BaseRequestModel, IRequest<PagedList<OrderBaseViewModel>>
    {
        public string PhoneNumber { get; set; }
    }
}
