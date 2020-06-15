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

    public class StationDeleteHandler : IRequestHandler<StationDeleteRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;

        public StationDeleteHandler(ApplicationDbContext db, UserManager<User> userManager)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.userManager = userManager;
        }

        public async Task<ResponseModel> Handle(StationDeleteRequest request, CancellationToken cancellationToken)
        {
            var station = await this.db.Stations.FirstOrDefaultAsync(x => x.Id == request.Id);

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
                    Message = "Bạn không thể xoá cửa hàng của người khác"
                };
            }

            station.IsDeleted = true;
            station.DeletedOn = DateTime.Now;

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Xoá cửa hàng thành công"
            };
        }
    }
}
