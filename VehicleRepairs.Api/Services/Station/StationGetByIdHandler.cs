namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Station.Models;

    public class StationGetByIdHandler : IRequestHandler<StationGetByIdRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public StationGetByIdHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(StationGetByIdRequest request, CancellationToken cancellationToken)
        {
            var station = await this.db.Stations
                .Where(x => x.Id == request.Id && !x.IsDeleted)
                    .Include(x => x.User)
                    .Include(x => x.Services)
                            .FirstOrDefaultAsync();

            if (station == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy cửa hàng này"
                };
            }

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new StationDetailViewModel(station)
            };
        }
    }
}
