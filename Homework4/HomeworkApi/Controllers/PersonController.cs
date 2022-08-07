using AutoMapper;
using Homework.Base;
using HomeworkApi.Data;
using HomeworkApi.Dto;
using HomeworkApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeworkApi
{
    [Route("api/v1/homework/[controller]")]
    [ApiController]
    public class PersonController : BaseController<PersonDto, Person>
    {
        private readonly IPersonService personService;
        private readonly IMemoryCache memoryCache;

        public PersonController(IPersonService personService, IMapper mapper, IMemoryCache memoryCache) : base(personService, mapper)
        {
            this.personService = personService;
            this.memoryCache = memoryCache;

        }

        [HttpGet]
        public async Task<IActionResult> GetPaginationAsync([FromQuery] string cacheKey,[FromQuery] int page, [FromQuery] int pageSize)
        {

            if (!memoryCache.TryGetValue(cacheKey, out var casheList))
            {
                PersonDto query = new PersonDto()
                {
                    StaffId = "test",
                    AccountId = 5,
                    FirstName = "test",
                    LastName = "test"
                };

                QueryResource pagintation = new QueryResource(page, pageSize);
                var result = await personService.GetPaginationAsync(pagintation, null);

                if (!result.Success)
                    return BadRequest(result);

                if (result.Response is null)
                    return NoContent();

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };

                // set cashe
                memoryCache.Set(cacheKey, result, cacheExpOptions);
                return Ok(result);
            }
            return Ok(casheList);   
        }

        [Route("GetAll")]
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync([FromQuery] string cacheKey)
        {
            if (!memoryCache.TryGetValue(cacheKey, out var casheList))
            {
                var result = await personService.GetAllAsync();

                if (!result.Success)
                    return BadRequest(result);

                if (result.Response is null)
                    return NoContent();

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };

                // set cashe
                memoryCache.Set(cacheKey, result, cacheExpOptions);
                return Ok(result);
            }
            return Ok(casheList);
        }
    }
}
