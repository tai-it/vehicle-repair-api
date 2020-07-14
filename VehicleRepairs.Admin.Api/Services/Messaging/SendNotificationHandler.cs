namespace VehicleRepairs.Admin.Api.Services.Messaging
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

    public class SendNotificationHandler : IRequestHandler<SendNotificationRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IFCMService fcmService;

        public SendNotificationHandler(ApplicationDbContext db, IMapper mapper, IFCMService fcmService)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.fcmService = fcmService;
        }

        public async Task<ResponseModel> Handle(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            var receiver = await this.db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (receiver == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Tài khoản này không tìm thấy hoặc đã bị khoá"
                };
            }

            var notify = this.mapper.Map<Notification>(request);

            notify.User = receiver;
            //notify.Target = receiver.DeviceToken;

            var isSent = await this.fcmService.SendNotification(notify);

            if (isSent)
            {
                notify.IsSent = true;
            }

            this.db.Notifications.Add(notify);
            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = isSent ? "Đã gửi thông báo thành công" : "Thông báo đã được lưu trữ"
            };
        }
    }
}
