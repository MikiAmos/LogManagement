using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> InsertAsync(T input,CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> UpdateAsync(T input,CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> DeleteAsync(int input,CancellationToken cancellationToken = default(CancellationToken));
    }
}