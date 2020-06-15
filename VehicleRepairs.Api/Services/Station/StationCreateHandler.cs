namespace VehicleRepairs.Api.Services.Station
{
    using AutoMapper;
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

    public class StationCreateHandler : IRequestHandler<StationCreateRequest, ResponseModel>
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;

        public StationCreateHandler(ApplicationDbContext db, IMapper mapper, UserManager<User> userManager)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userManager = userManager;
        }

        public async Task<ResponseModel> Handle(StationCreateRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            var station = await this.db.Stations.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (station != null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Xin lỗi, bạn đã tạo cửa hàng trước đó. Vui lòng thử lại bằng với cập nhật"
                };
            }

            station = this.mapper.Map<Station>(request);

            station.UserId = user.Id;

            this.db.Stations.Add(station);

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Data = new StationBaseViewModel(station)
            };
        }
    }
}
