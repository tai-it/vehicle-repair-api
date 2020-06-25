namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class ServiceDeleteHandler : IRequestHandler<ServiceDeleteRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public ServiceDeleteHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(ServiceDeleteRequest request, CancellationToken cancellationToken)
        {
            var service = await this.db.Services.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (service == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy dịch vụ này"
                };
            }

            var station = await this.db.Stations
                    .Include(x => x.User)
                        .FirstOrDefaultAsync(x => x.User.PhoneNumber == request.PhoneNumber);

            if (station == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Vui lòng tạo cửa hàng trước khi thực hiện thêm, sửa hoặc xoá dịch vụ"
                };
            }
            else if (service.StationId != station.Id)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden,
                    Message = "Bạn không thể xoá dịch vụ của cửa hàng khác"
                };
            }

            service.IsDeleted = true;
            service.DeletedOn = DateTime.Now;

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Xoá dịch vụ thành công"
            };
        }
    }
}
