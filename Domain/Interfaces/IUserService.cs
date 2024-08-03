using Domain.Dtos;
using Domain.Dtos.Insert;
using Domain.Dtos.Patch;
using Domain.Dtos.Search;
using Domain.Dtos.Update;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserService 
        : ICRUDService<UserDto, UserSearch, UserInsertDto, UserUpdateDto, UserPatchDto>
    {
    }
}