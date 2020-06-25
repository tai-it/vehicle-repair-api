namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using VehicleRepairs.Shared.Common;

    public class GetDistinctServiceByVehicleRequest : IRequest<ResponseModel>
    {
        public string Vehicle { get; set; }
    }
}
