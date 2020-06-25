namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Services.Station.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class GetMineStationHandler : IRequestHandler<GetMineStationRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public GetMineStationHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(GetMineStationRequest request, CancellationToken cancellationToken)
        {
            var stations = await this.db.Stations
                .Where(x => x.User.PhoneNumber == request.PhoneNumber)
                    .Select(x => new StationBaseViewModel(x))
                        .ToListAsync();
            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = stations
            };
        }
    }
}
