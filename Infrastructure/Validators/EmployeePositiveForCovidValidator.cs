using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Repository;
using Application.Services;
using Application.Validators;
using Domain;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Validators
{
    public class EmployeePositiveForCovidValidator:ILogRecordValidator
    {
        private readonly IRepository<Employee> _repository;
        private readonly ILogManagementRepository _logManagementRepository;


        public EmployeePositiveForCovidValidator(IRepository<Employee> repository,ILogManagementRepository logManagementRepository)
        {
            _repository = repository;
            _logManagementRepository = logManagementRepository;
        }

        public async Task<IEnumerable<NotificationInfo>> GetAllRelevantEmployees(LogInfo logInfo,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!logInfo.IsPositiveForCorona)
            {
                return new List<NotificationInfo>();
            }

            var date = logInfo.TimeStamp.Date;
            var dayOfLastWeek = date.AddDays(-(int) date.DayOfWeek - 6);
            var employeeIds =
                await _logManagementRepository.GetAllEmployeesFromLastWeek(dayOfLastWeek, cancellationToken);
            var allEmployees = await _repository.GetAllAsync(cancellationToken);
            var notifications = (from employee in allEmployees
                where employeeIds.Contains(employee.EmployeeId)
                select new NotificationInfo()
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    EmailAddress = employee.Email,
                    NotificationType = NotificationType.Covid
                });
            return notifications;
        }
    }
}