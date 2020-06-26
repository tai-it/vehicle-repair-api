namespace VehicleRepairs.Api.Infrastructure.Utilities
{
    using AutoMapper;
    using VehicleRepairs.Admin.Api.Services.Messaging;
    using VehicleRepairs.Database.Domain.Entities;

    //using VehicleRepairs.Api.Services.Ordering;
    //using VehicleRepairs.Api.Services.Ordering.Models;
    //using VehicleRepairs.Api.Services.Service;
    //using VehicleRepairs.Api.Services.Station;
    //using VehicleRepairs.Database.Domain.Entities;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<SendNotificationRequest, Notification>();
            //CreateMap<ServiceCreateRequest, Service>();
            //CreateMap<OrderCreateRequest, Order>();
            //CreateMap<OrderDetailCreateModel, OrderDetail>();
        }
    }
}
