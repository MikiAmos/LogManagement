using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController:ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken cancellationToken)
        {
            var query = new GetAllEmployeesQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpGet("{employeeId}")]
        public async Task<Employee> GetEmployee(int employeeId,CancellationToken cancellationToken)
        {
            var query = new GetSingleEmployeeQuery() {EmployeeId = employeeId};
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<int> AddNewEmployee([FromBody]Employee employee,CancellationToken cancellationToken)
        {
            var command = new AddNewEmployeeCommand() {Employee = employee};
            var id = await _mediator.Send(command, cancellationToken);
            return id;
        }
    }
}