namespace Microservices_Net6.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateDepartmentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateDepartmentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}