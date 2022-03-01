using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllEmployeesQuery:IRequest<IEnumerable<Employee>>
    {
        
    }
}