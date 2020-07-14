namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Services.Messaging;
    using VehicleRepairs.Api.Services.Ordering.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

    public class OrderEditHandler : IRequestHandler<OrderEditRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;
        private readonly IFCMService fcmService;
        private readonly UserManager<User> userManager;

        public OrderEditHandler(ApplicationDbContext db, IFCMService fcmService, UserManager<User> userManager)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.fcmService = fcmService ?? throw new ArgumentNullException(nameof(fcmService));
            this.userManager = userManager;
        }

        public async Task<ResponseModel> Handle(OrderEditRequest request, CancellationToken cancellationToken)
        {
            var order = await this.db.Orders
                    .Include(x => x.OrderDetails)
                    .Include(x => x.Station)
                        .ThenInclude(x => x.User)
                            .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy cuốc xe này"
                };
            }

            var requestUser = await userManager.Users
                    .Where(x => x.PhoneNumber == request.PhoneNumber)
                        .FirstOrDefaultAsync();

            var requestRoles = await userManager.GetRolesAsync(requestUser);

            if (!IsAcceptableStatus(order.Status, request.Status) || !IsRolePermitted(requestRoles.ToList(), request.Status))
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden,
                    Message = "Bạn không được phép thực hiện hành động này (Tình trạng cuốc xe không phù hợp)"
                };
            }

            if (requestRoles.Contains(CommonConstants.Roles.STATION) && order.Station.User.PhoneNumber != requestUser.PhoneNumber)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden,
                    Message = "Bạn không thể cập nhật cuốc xe của cửa hàng khác"
                };
            }

            var isStatusChange = false;

            if (order.Status != request.Status)
            {
                order.Status = request.Status;
                isStatusChange = true;
            }

            if (request.OrderDetails.Any())
            {
                var removingServices = order.OrderDetails.Where(x => !request.OrderDetails.Any(y => y.ServiceId == x.ServiceId)).ToList();

                foreach (var service in removingServices)
                {
                    order.OrderDetails.Remove(service);
                }

                order.OrderDetails.AddRange(request.OrderDetails.Where(x => !order.OrderDetails.Any(y => y.ServiceId == x.ServiceId))
                    .Select(x => new OrderDetail()
                    {
                        OrderId = order.Id,
                        ServiceId = x.ServiceId
                    }).ToList());
            }

            await this.db.SaveChangesAsync(cancellationToken);

            order = await this.db.Orders
                    .Include(x => x.User)
                    .Include(x => x.Station)
                        .ThenInclude(x => x.User)
                    .Include(x => x.OrderDetails)
                        .ThenInclude(x => x.Service)
                    .FirstOrDefaultAsync(x => x.Id == order.Id);

            if (isStatusChange)
            {
                await fcmService.SendNotifications(fcmService.GetNotificationsByOrder(order));
            }

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new OrderDetailViewModel(order)
            };
        }

        private bool IsAcceptableStatus(string oldStatus, string newStatus)
        {
            var acceptableStatus = new List<string>();
            switch (oldStatus)
            {
                case CommonConstants.OrderStatus.WAITING:
                    acceptableStatus = new List<string>
                    {
                        CommonConstants.OrderStatus.WAITING,
                        CommonConstants.OrderStatus.ACCEPTED,
                        CommonConstants.OrderStatus.REJECTED,
                        CommonConstants.OrderStatus.CANCLED
                    };
                    break;

                case CommonConstants.OrderStatus.ACCEPTED:
                    acceptableStatus = new List<string>
                    {
                        CommonConstants.OrderStatus.ACCEPTED,
                        CommonConstants.OrderStatus.DONE,
                        CommonConstants.OrderStatus.REJECTED,
                        CommonConstants.OrderStatus.CANCLED
                    };
                    break;

                default:
                    return false;
            }

            return acceptableStatus.Any(s => s.Contains(newStatus));
        }

        private bool IsRolePermitted(List<string> roles, string newStatus)
        {
            var acceptableStatus = new List<string>();

            if (roles.Contains(CommonConstants.Roles.STATION))
            {
                acceptableStatus = new List<string>
                {
                    CommonConstants.OrderStatus.ACCEPTED,
                    CommonConstants.OrderStatus.REJECTED,
                    CommonConstants.OrderStatus.DONE
                };
            }
            else if (roles.Contains(CommonConstants.Roles.USER))
            {
                acceptableStatus = new List<string>
                {
                    CommonConstants.OrderStatus.WAITING,
                    CommonConstants.OrderStatus.CANCLED
                };
            }

            return acceptableStatus.Any(s => s.Contains(newStatus));
        }
    }
}
