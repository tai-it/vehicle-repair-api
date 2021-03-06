﻿namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Services.Station.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

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

            if (!string.IsNullOrEmpty(request.Address))
            {
                station.Address = request.Address;
                station.Longitude = request.Longitude;
                station.Latitude = request.Latitude;
            }

            station.Vehicle = request.Vehicle ?? station.Vehicle;

            station.IsAvailable = request.IsAvailable;
            station.HasAmbulatory = request.HasAmbulatory;
            station.Coefficient = request.Coefficient;

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new StationDetailViewModel(station)
            };
        }
    }
}
