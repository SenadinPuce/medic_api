using Domain.Dtos.Search;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TDto,TSearch>(IReadService<TDto, TSearch> service) 
        : ControllerBase
          where TDto : class 
          where TSearch : BaseSearch
    {
        private readonly IReadService<TDto, TSearch> _service = service;

        [HttpGet]
        public virtual async Task<ActionResult<IReadOnlyList<TDto>>> Get([FromQuery] TSearch search)
        {
            var list = await _service.GetAllAsync(search);

            return Ok(list);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDto>> GetById(string id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null) return NotFound();

            return Ok(item);
        }
    }
}