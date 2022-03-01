using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class AddNewEmployeeCommand:IRequest<int>
    {
        public Employee Employee { get; set; }
    }
}