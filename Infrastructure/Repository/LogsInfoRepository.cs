using System;
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
    public class LogsInfoRepository:BaseRepository<LogInfoDto>,ILogManagementRepository
    {
        public LogsInfoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        protected override string CollectionName { get; } = DbCollectionNames.LogInfoCollection;
        public async Task<LogInfo> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var logs = await GetCollection().AsQueryable().Where(x => x.LogId == id)
                .FirstOrDefaultAsync(cancellationToken);
            return logs.Adapt<LogInfo>();
        }

        public async Task<IEnumerable<LogInfo>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var collection = await GetCollection().AsQueryable().ToListAsync(cancellationToken);
            return collection.Adapt<IEnumerable<LogInfo>>();
        }

        public async Task<int> InsertAsync(LogInfo input, CancellationToken cancellationToken = default(CancellationToken))
        {
            var collection = GetCollection();
            var lastId = collection.AsQueryable().Count();
            var newItem = input.Adapt<LogInfoDto>();
            newItem.LogId = lastId + 1;
            newItem.TimeStamp = DateTime.Now;
            await collection.InsertOneAsync(newItem, null, cancellationToken);
            return newItem.LogId;
        }

        public Task<bool> UpdateAsync(LogInfo input, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(int input, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<LogInfo>> GetAllLogsForEmployee(int employeeId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var logs = await GetCollection().AsQueryable().Where(x => x.EmployeeId == employeeId)
                .ToListAsync(cancellationToken);
            return logs.Adapt<IEnumerable<LogInfo>>();
        }

        public async Task<IEnumerable<int>> GetAllEmployeesFromLastWeek(DateTime lastWeek, CancellationToken cancellationToken = default(CancellationToken))
        {
            var employeeIds = await GetCollection().AsQueryable()
                .Where(x => x.TimeStamp < DateTime.Now && x.TimeStamp > lastWeek)
                .Select(x => x.EmployeeId).ToListAsync(cancellationToken);
            return employeeIds;
        }
    }
}