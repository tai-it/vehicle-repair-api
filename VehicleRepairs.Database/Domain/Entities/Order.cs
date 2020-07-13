namespace VehicleRepairs.Database.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using VehicleRepairs.Shared.Common;

    [Table("Orders")]
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public Guid StationId { get; set; }

        public Station Station { get; set; }

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

        public decimal AmbulatoryFee
        {
            get
            {
                if (UseAmbulatory)
                {
                    return this.Distance * CommonConstants.Ambulatory.COEFFICIENT;
                }
                return 0;
            }
        }

        [Required]
        public string Status { get; set; }

        public decimal? TotalPrice
        {
            get
            {
                if (OrderDetails.Any())
                {
                    decimal totalPrice = 0;

                    foreach(var orderDetail in OrderDetails)
                    {
                        totalPrice += orderDetail.Service.Price;
                    }

                    return totalPrice + this.AmbulatoryFee;
                }
                return null;
            }
        }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
