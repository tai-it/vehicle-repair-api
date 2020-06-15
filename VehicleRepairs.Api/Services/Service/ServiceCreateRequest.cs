namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using System.ComponentModel.DataAnnotations;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class ServiceCreateRequest : IRequest<ResponseModel>
    {
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
