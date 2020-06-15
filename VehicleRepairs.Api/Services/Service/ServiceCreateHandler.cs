namespace VehicleRepairs.Api.Services.Service
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Domain.Entities;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Service.Models;

    public class ServiceCreateHandler : IRequestHandler<ServiceCreateRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ServiceCreateHandler(ApplicationDbContext db, IMapper mapper, UserManager<User> userManager)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(ServiceCreateRequest request, CancellationToken cancellationToken)
        {
            var station = await this.db.Stations
                .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.User.PhoneNumber == request.PhoneNumber);

            if (station == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Vui lòng tạo cửa hàng trước khi thêm dịch vụ"
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
