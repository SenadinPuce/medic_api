using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Insert;
using Domain.Dtos.Patch;
using Domain.Dtos.Search;
using Domain.Dtos.Update;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services
{
    public class UserService(MedicLabContext context, IMapper mapper)
    : BaseCRUDService<UserDto, User, UserSearch, UserInsertDto, UserUpdateDto
    , UserPatchDto>(context, mapper), IUserService
    {
        private readonly MedicLabContext _context = context;
        public override async Task<List<UserDto>> GetAllAsync(UserSearch search)
        {
            var query = _context.Users.AsQueryable();

            query = AddFilter(query, search);
            query = AddInclude(query, search);
            query = AddSorting(query, search);

            if (search?.PageIndex.HasValue == true && search?.PageSize.HasValue == true)
            {
                query = query.Skip((search.PageIndex.Value - 1) * search.PageSize.Value).Take(search.PageSize.Value);
            }

            var list = await query
            .Select(x => new UserDto
            {
                Id = x.Id,
                Username = x.UserName!,
                Name = x.Name,
                LastLoginDate = x.LastLoginDate
            }).ToListAsync();

            return list;
        }

    }


}