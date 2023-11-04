namespace Core.Model
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }
        public int EmployeeId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int SiteId { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
        public Site Site { get; set; }
        public Location Location { get; set; }
        public Department Department { get; set; }
        
    }
}