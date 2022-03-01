using System;
using MongoDB.Bson;

namespace Infrastructure.Models
{
    public class LogInfoDto
    {
        public ObjectId Id { get; set; }
        public int LogId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int EmployeeId { get; set; }
        public bool IsArrived { get; set; }
        public bool IsPositiveForCorona { get; set; }
    }
}