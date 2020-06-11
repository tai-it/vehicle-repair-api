namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class StationDeleteHandler : IRequestHandler<StationDeleteRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public StationDeleteHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(StationDeleteRequest request, CancellationToken cancellationToken)
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

            station.IsDeleted = true;
            station.DeletedOn = DateTime.Now;

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Station deleted successfully"
            };
        }
    }
}
