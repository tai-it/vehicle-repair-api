namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Notification;

    public class OrderEditHandler : IRequestHandler<OrderEditRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;
        private readonly IFCMService fcmService;

        public OrderEditHandler(ApplicationDbContext db, IFCMService fcmService)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.fcmService = fcmService ?? throw new ArgumentNullException(nameof(fcmService));
        }

        public async Task<ResponseModel> Handle(OrderEditRequest request, CancellationToken cancellationToken)
        {
            var order = await this.db.Orders.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Order not found"
                };
            }

            order.Status = request.Status;

            await this.db.SaveChangesAsync(cancellationToken);

            order = await this.db.Orders
                    .Include(x => x.User)
                    .Include(x => x.Station)
                        .ThenInclude(x => x.User)
                    .Include(x => x.OrderDetails)
                        .ThenInclude(x => x.Service)
                    .FirstOrDefaultAsync(x => x.Id == order.Id);

            fcmService.SendToDevice(order);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Order updated successfully"
            };
        }
    }
}
