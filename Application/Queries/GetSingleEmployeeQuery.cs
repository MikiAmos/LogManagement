using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetSingleEmployeeQuery:IRequest<Employee>
    {
        public int EmployeeId { get; set; }
    }
}