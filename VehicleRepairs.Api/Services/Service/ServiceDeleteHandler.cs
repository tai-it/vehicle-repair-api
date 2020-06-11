namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;

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
                    Message = "Service not found"
                };
            }

            service.IsDeleted = true;
            service.DeletedOn = DateTime.Now;

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = "Service deleted successfully"
            };
        }
    }
}
