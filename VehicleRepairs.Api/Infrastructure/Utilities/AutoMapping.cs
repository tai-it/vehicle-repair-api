namespace VehicleRepairs.Api.Infrastructure.Utilities
{
    using AutoMapper;
    using VehicleRepairs.Api.Domain.Entities;
    using VehicleRepairs.Api.Services.Ordering;
    using VehicleRepairs.Api.Services.Ordering.Models;
    using VehicleRepairs.Api.Services.Service;
    using VehicleRepairs.Api.Services.Station;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<StationCreateRequest, Station>();
            CreateMap<ServiceCreateRequest, Service>();
            CreateMap<OrderCreateRequest, Order>();
            CreateMap<OrderDetailCreateModel, OrderDetail>();
        }
    }
}
