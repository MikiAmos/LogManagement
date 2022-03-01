using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;

namespace Application.Validators
{
    public interface ILogRecordValidator
    {
        Task<IEnumerable<NotificationInfo>> GetAllRelevantEmployees(LogInfo logInfo,CancellationToken cancellationToken = default(CancellationToken));
    }
}