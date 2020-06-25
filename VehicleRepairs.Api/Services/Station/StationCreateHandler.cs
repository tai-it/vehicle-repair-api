namespace VehicleRepairs.Api.Services.Station
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Services.Station.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

    public class StationCreateHandler : IRequestHandler<StationCreateRequest, ResponseModel>
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;

        public StationCreateHandler(ApplicationDbContext db, IMapper mapper, UserManager<User> userManager)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userManager = userManager;
        }

        public async Task<ResponseModel> Handle(StationCreateRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            var station = this.mapper.Map<Station>(request);

            station.User = user;

            this.db.Stations.Add(station);

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Data = new StationBaseViewModel(station)
            };
        }
    }
}
