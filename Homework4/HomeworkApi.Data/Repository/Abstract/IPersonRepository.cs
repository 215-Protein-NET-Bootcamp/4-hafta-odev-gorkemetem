using Homework.Base;
using HomeworkApi.Data;
using HomeworkApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework.Data
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<(IEnumerable<Person> records, int total)> GetPaginationAsync(QueryResource pagination, PersonDto filterResource);
        Task<int> TotalRecordAsync();
    }
}
