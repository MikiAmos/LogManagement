using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Application.Repository;
using Application.Services;
using Application.Validators;
using Domain;
using Domain.Entities;
using MediatR;

namespace Application.Handlers
{
    public class LogInfoHandler:IRequestHandler<GetLogInfoForEmployee,IEnumerable<LogInfo>>,
        IRequestHandler<AddNewLogInfoCommand,int>
    {
        private readonly ILogManagementRepository _repository;
        private readonly IEnumerable<ILogRecordValidator> _validators;
        private readonly INotificationService _notificationService;

        public LogInfoHandler(ILogManagementRepository repository,IEnumerable<ILogRecordValidator> validators,INotificationService notificationService)
        {
            _repository = repository;
            _validators = validators;
            _notificationService = notificationService;
        }
        
        public async Task<IEnumerable<LogInfo>> Handle(GetLogInfoForEmployee request, CancellationToken cancellationToken)
        {
            var allRelevantLogs = await _repository.GetAllLogsForEmployee(request.EmployeeId, cancellationToken);
            return allRelevantLogs;
        }

        public async Task<int> Handle(AddNewLogInfoCommand request, CancellationToken cancellationToken)
        {
            var id = await _repository.InsertAsync(request.LogInfo, cancellationToken);
            var notifications = new List<NotificationInfo>();
            foreach (var validator in _validators)
            {
                var notificationsResult = await validator.GetAllRelevantEmployees(request.LogInfo, cancellationToken);
                notifications.AddRange(notificationsResult);
            }

            if (notifications.Any())
                await _notificationService.NotifyEmployees(notifications, cancellationToken);
           
            return id;
        }
    }
}