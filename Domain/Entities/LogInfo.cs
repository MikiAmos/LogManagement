using System;

namespace Domain.Entities
{
    public class LogInfo
    {
        public int LogId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int EmployeeId { get; set; }
        public bool IsArrived { get; set; }
        public bool IsPositiveForCorona { get; set; }
    }
}