namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using VehicleRepairs.Api.Services.Ordering.Models;
    using VehicleRepairs.Shared.Common;

    public class OrderEditRequest : IRequest<ResponseModel>
    {

        public OrderEditRequest()
        {
            OrderDetails = new List<OrderDetailCreateModel>();
        }

        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Status { get; set; }

        public List<OrderDetailCreateModel> OrderDetails { get; set; }
    }
}
