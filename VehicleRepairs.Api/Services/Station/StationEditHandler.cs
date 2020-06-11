namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class StationEditHandler : IRequestHandler<StationEditRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public StationEditHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(StationEditRequest request, CancellationToken cancellationToken)
        {
            var station = await this.db.Stations.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (station == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Station not found"
                };
            }

            station.Name = request.Name ?? station.Name;
            station.Address = request.Address ?? station.Address;

            if (request.Longitude != decimal.Zero)
            {
                station.Longitude = request.Longitude;
            }

            if (request.Latitude != decimal.Zero)
            {
                station.Latitude = request.Latitude;
            }

            station.Vehicle = request.Vehicle ?? station.Vehicle;

            station.IsAvailable = request.IsAvailable;
            station.HasAmbulatory = request.HasAmbulatory;

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Station updated successfully"
            };
        }
    }
}
