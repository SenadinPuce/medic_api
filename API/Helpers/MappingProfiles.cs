using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Insert;
using Domain.Dtos.Patch;
using Domain.Dtos.Update;
using Domain.Models;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserInsertDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserPatchDto, User>();
        }

    }
}