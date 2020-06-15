namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class GetDistinctServiceByVehicleHandler : IRequestHandler<GetDistinctServiceByVehicleRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public GetDistinctServiceByVehicleHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(GetDistinctServiceByVehicleRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Services
                        .Include(x => x.Station)
                            .Where(x => !x.IsDeleted && x.Station.Vehicle.ToLower().Equals(request.Vehicle.ToLower()))
                                .ToListAsync();

            var services = list
                        .GroupBy(x => x.Name)
                            .Select(g => g.First())
                                .Select(x => x.Name)
                                    .ToList();

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = services
            };
        }
    }
}
