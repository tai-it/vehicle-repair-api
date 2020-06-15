namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class ServiceEditHandler : IRequestHandler<ServiceEditRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public ServiceEditHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(ServiceEditRequest request, CancellationToken cancellationToken)
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
                    Message = "Bạn không thể cập nhật dịch vụ của người khác"
                };
            }

            service.Name = request.Name ?? service.Name;
            service.Description = request.Description ?? service.Description;
            service.Thumbnail = request.Thumbnail ?? service.Thumbnail;

            if (request.Price != decimal.Zero)
            {
                service.Price = request.Price;
            }

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Cập nhật dịch vụ thành công"
            };
        }
    }
}
