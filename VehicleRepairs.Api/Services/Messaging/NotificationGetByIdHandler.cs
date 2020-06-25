namespace VehicleRepairs.Api.Services.Messaging
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Services.Messaging.Models;
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
                .Where(x => x.User.PhoneNumber == request.PhoneNumber && x.Id == request.Id)
                    .Include(x => x.User)
                    .Include(x => x.Order)
                        .ThenInclude(x => x.OrderDetails)
                            .ThenInclude(x => x.Service)
                    .Include(x => x.Order)
                        .ThenInclude(x => x.Station)
                                .FirstOrDefaultAsync();

            if (notification == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Thông báo không tìm thấy hoặc đã bị xoá"
                };
            }

            notification.IsSeen = true;

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new NotificationDetailViewModel(notification)
            };
        }
    }
}
