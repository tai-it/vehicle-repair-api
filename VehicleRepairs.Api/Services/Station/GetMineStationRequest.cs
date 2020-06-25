namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using VehicleRepairs.Shared.Common;

    public class GetMineStationRequest : IRequest<ResponseModel>
    {
        public string PhoneNumber { get; set; }
    }
}
