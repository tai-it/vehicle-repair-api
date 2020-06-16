namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using System;
    using System.ComponentModel.DataAnnotations;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class ServiceCreateRequest : IRequest<ResponseModel>
    {
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn cửa hàng")]
        public Guid StationId { get; set; }

        [Required(ErrorMessage = "Vui lòng điền tên dịch vụ")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        [Required(ErrorMessage = "Vui lòng điền giá dịch vụ")]
        public decimal Price { get; set; }
    }
}
