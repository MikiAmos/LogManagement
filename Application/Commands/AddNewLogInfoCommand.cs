using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class AddNewLogInfoCommand:IRequest<int>
    {
        public LogInfo LogInfo { get; set; }
    }
}