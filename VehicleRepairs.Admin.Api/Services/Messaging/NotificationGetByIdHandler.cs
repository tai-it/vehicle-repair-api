namespace VehicleRepairs.Admin.Api.Services.Messaging
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Services.Messaging.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class NotificationGetByIdHandler : IRequestHandler<NotificationGetByIdRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public NotificationGetByIdHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(NotificationGetByIdRequest request, CancellationToken cancellationToken)
        {
            var notification = await this.db.Notifications
                .Where(x => x.Id == request.Id)
                    .Include(x => x.User)
                        .FirstOrDefaultAsync();

            if (notification == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Thông báo không tìm thấy hoặc đã bị xoá"
                };
            }

            if (notification.Type == CommonConstants.NotificationTypes.ORDER_TRACKING)
            {
                var order = await this.db.Orders
                    .Where(x => x.Id == new Guid(notification.Data))
                    .Include(x => x.User)
                        .Include(x => x.Station)
                    .Include(x => x.OrderDetails)
                        .ThenInclude(x => x.Service)
                    .FirstOrDefaultAsync();

                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new NotificationDetailViewModel(notification, order)
                };
            }

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new NotificationBaseViewModel(notification)
            };
        }
    }
}
