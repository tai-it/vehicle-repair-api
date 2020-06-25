namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Services.Service.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class ServiceGetByIdHandler : IRequestHandler<ServiceGetByIdRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public ServiceGetByIdHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(ServiceGetByIdRequest request, CancellationToken cancellationToken)
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

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new ServiceViewModel(service)
            };
        }
    }
}
