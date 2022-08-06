using Homework.Base;
using HomeworkApi.Data;
using HomeworkApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeworkApi.Service
{
    public interface IPersonService : IBaseService<PersonDto, Person>
    {
        Task<PaginationResponse<IEnumerable<PersonDto>>> GetPaginationAsync(QueryResource pagination, PersonDto filterResource);
    }
}
