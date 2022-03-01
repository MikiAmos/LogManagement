using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Application.Repository;
using Domain.Entities;
using MediatR;

namespace Application.Handlers
{
    public class EmployeeHandler:IRequestHandler<GetAllEmployeesQuery,IEnumerable<Employee>>,
        IRequestHandler<GetSingleEmployeeQuery,Employee>,
        IRequestHandler<AddNewEmployeeCommand,int>
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeHandler(IRepository<Employee> repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var allEmployees = await _repository.GetAllAsync(cancellationToken);
            return allEmployees;
        }

        public async Task<Employee> Handle(GetSingleEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetAsync(request.EmployeeId, cancellationToken);
            return employee;
        }

        public async Task<int> Handle(AddNewEmployeeCommand request, CancellationToken cancellationToken)
        {
            var newEmployeeId = await _repository.InsertAsync(request.Employee, cancellationToken);
            return newEmployeeId;
        }
    }
}