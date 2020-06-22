namespace VehicleRepairs.Api.Services.Messaging
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class NotificationMarkAllAsReadHandler : IRequestHandler<NotificationMarkAllAsReadRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public NotificationMarkAllAsReadHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(NotificationMarkAllAsReadRequest request, CancellationToken cancellationToken)
        {
            var notifications = await this.db.Notifications
                .Where(x => !x.IsSeen && x.User.PhoneNumber == request.PhoneNumber)
                    .ToListAsync();

            if (notifications.Any())
            {
                foreach (var notification in notifications)
                {
                    notification.IsSeen = true;
                }
                await this.db.SaveChangesAsync();
            }

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Thành công"
            };
        }
    }
}
