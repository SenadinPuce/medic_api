using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseCRUDController<TDto, TSearch, TInsert, TUpdate, TPatch>(ICRUDService<TDto, TSearch, TInsert, TUpdate, TPatch> service)
    : BaseController<TDto, TSearch>(service)
        where TDto : class
        where TSearch : Domain.Dtos.Search.BaseSearch
        where TInsert : class
        where TUpdate : class
        where TPatch : class
    {
        protected readonly ICRUDService<TDto, TSearch, TInsert, TUpdate, TPatch> _service = service;

        [HttpPost]
        public virtual async Task<ActionResult<TDto?>> Insert([FromBody] TInsert insert)
        {
            var item = await _service.InsertAsync(insert);

			if (item == null) return BadRequest();

			return item;
		}

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TDto>> Update(string id, [FromBody] TUpdate update)
        {
            var item = await _service.UpdateAsync(id, update);

            if (item == null) return NotFound();

            return item;
        }

        [HttpPatch("{id}")]
        public virtual async Task<ActionResult<TDto>> Patch(string id, [FromBody] TPatch patch)
        {
            var item = await _service.PatchAsync(id, patch);

            if (item == null) return NotFound();

            return item;
        }
    }
}