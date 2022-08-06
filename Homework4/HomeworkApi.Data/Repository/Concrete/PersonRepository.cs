using Homework.Base;
using Homework.Data;
using HomeworkApi.Base;
using HomeworkApi.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeworkApi.Data
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext Context) : base(Context)
        {
        }

        public async Task<(IEnumerable<Person> records, int total)> GetPaginationAsync(QueryResource pagination, PersonDto filterResource)
        {
            var queryable = ConditionFilter(filterResource);

            var total = await queryable.CountAsync();

            var records = await queryable.AsNoTracking()
                .AsSplitQuery()
                .OrderByDescending(x => x.AccountId)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return (records, total);
        }


        private IQueryable<Person> ConditionFilter(PersonDto filterResource)
        {
            var queryable = Context.person.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterResource.StaffId))
            {
                queryable.Where(x => x.StaffId == filterResource.StaffId);
            }

            if (filterResource != null)
            {

                if (!string.IsNullOrEmpty(filterResource.FirstName))
                {
                    string fullName = filterResource.FirstName.RemoveSpaceCharacter().ToLower();
                    queryable = queryable.Where(x => x.FirstName.Contains(fullName));
                }

                if (!string.IsNullOrEmpty(filterResource.LastName))
                {
                    string fullName = filterResource.LastName.RemoveSpaceCharacter().ToLower();
                    queryable = queryable.Where(x => x.LastName.Contains(fullName));
                }
            }

            return queryable;
        }

        public async Task<int> TotalRecordAsync()
        {
            return await Context.person.CountAsync();
        }
    }
}
