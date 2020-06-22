namespace VehicleRepairs.Api.Infrastructure.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActived { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
