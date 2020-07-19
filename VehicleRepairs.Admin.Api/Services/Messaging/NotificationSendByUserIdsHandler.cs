namespace VehicleRepairs.Admin.Api.Services.Messaging
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

    public class NotificationSendByUserIdsHandler : IRequestHandler<NotificationSendByUserIdsRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;
        private readonly IFCMService fcmService;

        public NotificationSendByUserIdsHandler(ApplicationDbContext db, IFCMService fcmService)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.fcmService = fcmService ?? throw new ArgumentNullException(nameof(fcmService)); ;
        }

        public async Task<ResponseModel> Handle(NotificationSendByUserIdsRequest request, CancellationToken cancellationToken)
        {
            var notifications = await this.db.Users
                .Where(x => request.Ids.Any(id => x.Id == id))
                .Select(x => new Notification()
                {
                    Type = CommonConstants.NotificationTypes.NOTIFY,
                    Title = request.Title,
                    Body = request.Body,
                    Data = request.Data,
                    User = x
                })
                .ToListAsync();

            await this.fcmService.SendNotifications(notifications);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Messages sent"
            };
        }
    }
}
