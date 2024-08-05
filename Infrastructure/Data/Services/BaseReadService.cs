using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Search;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services
{
    public class BaseReadService<TDto, TDb, TSearch>(MedicLabContext context, IMapper mapper) : IReadService<TDto, TSearch>
        where TDto : class
        where TDb : class
        where TSearch : BaseSearch
    {
        private readonly MedicLabContext _context = context;
        private readonly IMapper _mapper = mapper;

        public virtual async Task<PagedResult<TDto>> GetAllAsync(TSearch search)
        {
            var query = _context.Set<TDb>().AsQueryable();

            PagedResult<TDto> result = new();

			query = AddFilter(query, search);
            query = AddInclude(query, search);
            query = AddSorting(query, search);

            result.Count = await query.CountAsync();

			if (search?.PageIndex.HasValue == true && search?.PageSize.HasValue == true)
            {
                query = query.Skip((search.PageIndex.Value - 1) * search.PageSize.Value)
                    .Take(search.PageSize.Value);
            }

            var list = await query.ToListAsync();
            result.Items = _mapper.Map<List<TDto>>(list);

            return result;
        }

        public virtual async Task<TDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<TDb>().FindAsync(id);

            if (entity == null) return null;

            return _mapper.Map<TDto>(entity);
        }

        public virtual IQueryable<TDb> AddInclude(IQueryable<TDb> query, TSearch search)
        {
            return query;
        }

        public virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch search)
        {
            return query;
        }

        public virtual IQueryable<TDb> AddSorting(IQueryable<TDb> query, TSearch search)
        {
            return query;
        }
    }
}