namespace VehicleRepairs.Admin.Api.Services.Station
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Services.Messaging;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

    public class StationDeleteHandler : IRequestHandler<StationDeleteRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        private readonly IFCMService fcmService;

        public StationDeleteHandler(ApplicationDbContext db, IFCMService fcmService)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.fcmService = fcmService;
        }

        public async Task<ResponseModel> Handle(StationDeleteRequest request, CancellationToken cancellationToken)
        {
            var station = await this.db.Stations
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

            station.IsActived = false;
            station.UpdatedOn = DateTime.Now;

            await this.db.SaveChangesAsync(cancellationToken);

            var notify = new Notification()
            {
                Title = "Thông báo",
                Body = "Cửa hàng của bạn đã bị đình chỉ hoạt động khỏi hệ thống. Vui lòng liên hệ hỗ trợ viên để biết thêm chi tiết",
                User = station.User,
                Type = CommonConstants.NotificationTypes.NOTIFY
            };

            await fcmService.SendNotification(notify);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Xoá cửa hàng thành công"
            };
        }
    }
}
