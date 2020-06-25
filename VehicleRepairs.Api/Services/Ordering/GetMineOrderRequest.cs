namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using VehicleRepairs.Api.Services.Ordering.Models;
    using VehicleRepairs.Shared.Common;

    public class GetMineOrderRequest : BaseRequestModel, IRequest<PagedList<OrderDetailViewModel>>
    {
        public string PhoneNumber { get; set; }
    }
}
