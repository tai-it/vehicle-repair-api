namespace VehicleRepairs.Api.Services.Ordering
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Domain.Entities;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Notification;
    using VehicleRepairs.Api.Services.Ordering;
    using VehicleRepairs.Api.Services.Ordering.Models;

    public class OrderCreateHandler : IRequestHandler<OrderCreateRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IFCMService fcmService;

        public OrderCreateHandler(ApplicationDbContext db, IMapper mapper, IFCMService fcmService)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.fcmService = fcmService ?? throw new ArgumentNullException(nameof(fcmService));
        }

        public async Task<ResponseModel> Handle(OrderCreateRequest request, CancellationToken cancellationToken)
        {
            //var order = await this.db.Orders.FirstOrDefaultAsync(x => x.UserId == request.UserId && (x.Status != CommonConstants.OrderStatus.DONE && x.Status != CommonConstants.OrderStatus.REJECTED && x.Status != CommonConstants.OrderStatus.CANCLED));
        
            //if (order != null)
            //{
            //    return new ResponseModel()
            //    {
            //        StatusCode = System.Net.HttpStatusCode.BadRequest,
            //        Message = "Bạn đang đặt một cuốc xe chưa hoàn thành, vui lòng huỷ để đặt cuốc xe mới"
            //    };
            //}

            var order = this.mapper.Map<Order>(request);

            order.Status = CommonConstants.OrderStatus.WAITING;

            this.db.Orders.Add(order);

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
                Data = new OrderDetailViewModel(order)
            };
        }
    }
}
