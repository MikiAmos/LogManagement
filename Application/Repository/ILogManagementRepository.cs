using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repository
{
    public interface ILogManagementRepository:IRepository<LogInfo>
    {
        Task<IEnumerable<LogInfo>> GetAllLogsForEmployee(int employeeId,CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<int>> GetAllEmployeesFromLastWeek(DateTime lastWeek,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}