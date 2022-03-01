using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetLogInfoForEmployee:IRequest<IEnumerable<LogInfo>>
    {
        public int EmployeeId { get; set; }
    }
}