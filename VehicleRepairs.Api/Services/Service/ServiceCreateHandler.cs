namespace VehicleRepairs.Api.Services.Service
{
    using AutoMapper;
    using MediatR;
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

        public ServiceCreateHandler(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(ServiceCreateRequest request, CancellationToken cancellationToken)
        {
            var service = this.mapper.Map<Service>(request);

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
