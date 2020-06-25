namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using System;
    using VehicleRepairs.Shared.Common;

    public class ServiceEditRequest : IRequest<ResponseModel>
    {
        public string PhoneNumber { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public decimal Price { get; set; }
    }
}
