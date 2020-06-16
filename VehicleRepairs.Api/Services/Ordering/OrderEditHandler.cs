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
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Domain.Entities;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Notification;

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
                Data = "Cập nhật cuốc xe thành công"
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
                        CommonConstants.OrderStatus.ACCEPTED,
                        CommonConstants.OrderStatus.REJECTED,
                        CommonConstants.OrderStatus.CANCLED
                    };
                    break;

                case CommonConstants.OrderStatus.ACCEPTED:
                    acceptableStatus = new List<string>
                    {
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
