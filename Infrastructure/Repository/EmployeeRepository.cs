using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Models;
using Mapster;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Repository
{
    public class EmployeeRepository:BaseRepository<EmployeeDto>,IRepository<Employee>
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        protected override string CollectionName { get; } = DbCollectionNames.EmployeeCollection;
        public async Task<Employee> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var department = await GetCollection().AsQueryable().Where(x => x.EmployeeId == id)
                .FirstOrDefaultAsync(cancellationToken);
            return department.Adapt<Employee>();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var collection = await GetCollection().AsQueryable().ToListAsync(cancellationToken);
            return collection.Adapt<IEnumerable<Employee>>();
        }

        public async Task<int> InsertAsync(Employee input, CancellationToken cancellationToken = default(CancellationToken))
        {
            var collection = GetCollection();
            var lastId = collection.AsQueryable().Count();
            var newItem = input.Adapt<EmployeeDto>();
            newItem.EmployeeId = lastId + 1;
            await collection.InsertOneAsync(newItem, null, cancellationToken);
            return newItem.EmployeeId;
        }

        public async Task<bool> UpdateAsync(Employee input, CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<EmployeeDto>.Filter.Eq(nameof(input.EmployeeId), input.EmployeeId);
            var update = Builders<EmployeeDto>.Update.Set(nameof(input.Email), input.Email)
                .Set(nameof(input.FirstName), input.FirstName)
                .Set(nameof(input.LastName), input.LastName);
            var result = await GetCollection().UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(int input, CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<EmployeeDto>.Filter.Eq("EmployeeId", input);
            var result = await GetCollection().DeleteOneAsync(filter, cancellationToken);
            return result.IsAcknowledged;
        }
    }
}