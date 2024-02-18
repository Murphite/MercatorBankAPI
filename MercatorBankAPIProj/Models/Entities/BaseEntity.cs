namespace MercatorBankAPIProj.Models.Entities
{
    public class BaseEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

    }
}
