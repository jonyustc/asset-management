namespace Core.Model
{
    public class Warranty : BaseEntity
    {
        public int Length { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}