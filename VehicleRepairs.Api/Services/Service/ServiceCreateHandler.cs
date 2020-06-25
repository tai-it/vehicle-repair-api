namespace VehicleRepairs.Api.Services.Service
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Services.Service.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

    public class ServiceCreateHandler : IRequestHandler<ServiceCreateRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ServiceCreateHandler(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(ServiceCreateRequest request, CancellationToken cancellationToken)
        {
            var station = await this.db.Stations
                .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == request.StationId);

            if (station == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Cửa hàng này không tìm thấy hoặc đã bị xoá"
                };
            }

            if (station.User.PhoneNumber != request.PhoneNumber)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden,
                    Message = "Bạn không có quyền thêm dịch vụ cho cửa hàng khác"
                };
            }

            var service = this.mapper.Map<Service>(request);

            service.StationId = station.Id;

            this.db.Services.Add(service);

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new ServiceViewModel(service)
            };
        }
    }
}
