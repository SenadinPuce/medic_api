using Domain.Dtos;
using Domain.Dtos.Insert;
using Domain.Dtos.Patch;
using Domain.Dtos.Search;
using Domain.Dtos.Update;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController(IUserService service)
        : BaseCRUDController<UserDto, UserSearch, UserInsertDto, UserUpdateDto, UserPatchDto>(service)
    {
        [Authorize]
        public override Task<ActionResult<IReadOnlyList<UserDto>>> Get([FromQuery] UserSearch search)
        {
            return base.Get(search);
        }

        [Authorize]
        [HttpGet("details/{id}")]
        public override Task<ActionResult<UserDto>> GetById(string id)
        {
            return base.GetById(id);
        }

        [Authorize]
        [HttpPost("add")]
        public override Task<ActionResult<UserDto>> Insert([FromBody] UserInsertDto insert)
        {
            return base.Insert(insert);
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public override Task<ActionResult<UserDto>> Update(string id, [FromBody] UserUpdateDto update)
        {
            return base.Update(id, update);
        }

        [Authorize]
        [HttpPatch("change-status/{id}")]
        public override Task<ActionResult<UserDto>> Patch(string id, [FromBody] UserPatchDto patch)
        {
            return base.Patch(id, patch);
        }
    }
}