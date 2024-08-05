using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Insert;
using Domain.Dtos.Patch;
using Domain.Dtos.Search;
using Domain.Dtos.Update;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services
{
	public class UserService(MedicLabContext context, IMapper mapper, UserManager<User> userManager)
    : BaseCRUDService<UserDto, User, UserSearch, UserInsertDto, UserUpdateDto
    , UserPatchDto>(context, mapper), IUserService
    {
        private readonly MedicLabContext _context = context;
		private readonly IMapper _mapper = mapper;
		private readonly UserManager<User> _userManager = userManager;

		public override async Task<PagedResult<UserDto>> GetAllAsync(UserSearch search)
        {
            var query = _context.Users.AsQueryable();

            query = AddFilter(query, search);
            query = AddInclude(query, search);
            query = AddSorting(query, search);

			PagedResult<UserDto> result = new();

            result.Count = await query.CountAsync();

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
                LastLoginDate = x.LastLoginDate,
				IsBlocked = x.IsBlocked,
			}).ToListAsync();

			result.Items = list;

			return result;
        }


		public override IQueryable<User> AddFilter(IQueryable<User> query, UserSearch search)
		{
			if(!string.IsNullOrWhiteSpace(search.Username))
            {
				query = query.Where(x => x.UserName!.Contains(search.Username));
			}
		
            return query;
		}

		public override async Task<UserDto?> InsertAsync(UserInsertDto insert)
		{
			User user = new();
        
            _mapper.Map(insert, user);

            var result = await _userManager.CreateAsync(user, insert.Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "employee");
				return _mapper.Map<UserDto>(user);
			}
            return null;
		}

		public override async Task<UserDto?> UpdateAsync(string id, UserUpdateDto update)
		{
			var entity = await _userManager.FindByIdAsync(id);

			if (entity == null) return null;

			_mapper.Map(update, entity);

			if (!string.IsNullOrWhiteSpace(update.Password))
			{
				var removePasswordResult = await _userManager.RemovePasswordAsync(entity);
				if (!removePasswordResult.Succeeded)
				{
					return null;
				}

				var addPasswordResult = await _userManager.AddPasswordAsync(entity, update.Password);
				if (!addPasswordResult.Succeeded)
				{
					return null;
				}
			}

			await _userManager.UpdateAsync(entity);

			return _mapper.Map<UserDto>(entity);
		}
	}


}