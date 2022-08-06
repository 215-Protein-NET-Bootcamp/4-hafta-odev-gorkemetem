using System;
using System.Threading.Tasks;

namespace HomeworkApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }
}
