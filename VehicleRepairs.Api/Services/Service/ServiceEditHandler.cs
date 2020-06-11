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
                    Message = "Service not found"
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
                Data = "Service updated successfully"
            };
        }
    }
}
