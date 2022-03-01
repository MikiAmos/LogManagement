using System.Collections.Generic;
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
    public class LogManagementController:ControllerBase
    {
        private readonly IMediator _mediator;
       

        public LogManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{employeeId}")]
        public async Task<IEnumerable<LogInfo>> GetAllLogs(int employeeId)
        {
            var query = new GetLogInfoForEmployee() {EmployeeId = employeeId};
            var allLogs = await _mediator.Send(query);
            return allLogs;
        }

        [HttpPost]
        public async Task<int> AddNewLogInfo([FromBody]LogInfo logInfo)
        {
            var command = new AddNewLogInfoCommand() {LogInfo = logInfo};
            var id = await _mediator.Send(command);
            return id;
        }
    }
}