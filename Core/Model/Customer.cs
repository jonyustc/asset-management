namespace Core.Model
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Copmany { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
    }
}