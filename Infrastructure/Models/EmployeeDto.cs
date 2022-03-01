using MongoDB.Bson;

namespace Infrastructure.Models
{
    public class EmployeeDto
    {
        public ObjectId Id { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}