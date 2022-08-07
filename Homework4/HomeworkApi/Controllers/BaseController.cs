using AutoMapper;
using HomeworkApi.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeworkApi
{

    /*
     * Base controller class with crud operations
     */
    [ApiController]
    public class BaseController<Dto, Entity> : ControllerBase
    {
        private readonly IBaseService<Dto, Entity> _baseService;
        protected readonly IMapper Mapper;


        public BaseController(IBaseService<Dto, Entity> baseService, IMapper mapper)
        {
            this._baseService = baseService;
            this.Mapper = mapper;
        }
    
    }
}
