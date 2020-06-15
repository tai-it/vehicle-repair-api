﻿using MediatR;
namespace VehicleRepairs.Api.Services.Service
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class ServiceDeleteRequest : IRequest<ResponseModel>
    {
        [Phone]
        public string PhoneNumber { get; set; }

        public Guid Id { get; set; }
    }
}
