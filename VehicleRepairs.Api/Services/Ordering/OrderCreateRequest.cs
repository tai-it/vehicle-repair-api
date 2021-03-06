﻿namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VehicleRepairs.Api.Services.Ordering.Models;
    using VehicleRepairs.Shared.Common;

    public class OrderCreateRequest : IRequest<ResponseModel>
    {
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public Guid StationId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        public int Distance { get; set; }

        [Required]
        public bool UseAmbulatory { get; set; }

        public List<OrderDetailCreateModel> OrderDetails { get; set; }
    }
}
