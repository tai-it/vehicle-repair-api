namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Domain.Entities;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Station.Models;

    public class StationEditHandler : IRequestHandler<StationEditRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;

        public StationEditHandler(ApplicationDbContext db, UserManager<User> userManager)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.userManager = userManager;
        }

        public async Task<ResponseModel> Handle(StationEditRequest request, CancellationToken cancellationToken)
        {
            var station = await this.db.Stations
                .Include(x => x.Services)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (station == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy cửa hàng này"
                };
            }

            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            if (station.UserId != user.Id)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden,
                    Message = "Bạn không thể cập nhật thông tin cửa hàng của người khác"
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
                Data = new StationDetailViewModel(station)
            };
        }
    }
}
