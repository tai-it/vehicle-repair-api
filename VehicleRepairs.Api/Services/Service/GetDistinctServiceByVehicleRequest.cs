namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class GetDistinctServiceByVehicleRequest : IRequest<ResponseModel>
    {
        public string Vehicle { get; set; }
    }
}
