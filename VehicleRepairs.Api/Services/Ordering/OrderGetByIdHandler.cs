namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Ordering.Models;

    public class OrderGetByIdHandler : IRequestHandler<OrderGetByIdRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public OrderGetByIdHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(OrderGetByIdRequest request, CancellationToken cancellationToken)
        {
            var order = await this.db.Orders
                     .Include(x => x.User)
                     .Include(x => x.Station)
                         .ThenInclude(x => x.User)
                     .Include(x => x.OrderDetails)
                         .ThenInclude(x => x.Service)
                     .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Order not found"
                };
            }

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new OrderDetailViewModel(order)
            };
        }
    }
}
