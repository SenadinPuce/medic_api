using AutoMapper;
using Domain.Dtos.Search;
using Domain.Interfaces;

namespace Infrastructure.Data.Services
{
    public class BaseCRUDService<TDto, TDb, TSearch, TInsert, TUpdate, TPatch>(MedicLabContext context, IMapper mapper)
    : BaseReadService<TDto, TDb, TSearch>(context, mapper), ICRUDService<TDto, TSearch, TInsert, TUpdate, TPatch>
        where TDto : class
        where TDb : class
        where TSearch : BaseSearch
        where TInsert : class
        where TUpdate : class
        where TPatch : class
    {
        private readonly MedicLabContext _context = context;
        private readonly IMapper _mapper = mapper;

        public virtual async Task<TDto?> InsertAsync(TInsert insert)
        {
            var set = _context.Set<TDb>();

            TDb entity = _mapper.Map<TDb>(insert);

            set.Add(entity);
            await BeforeInsert(entity, insert);

            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto?> PatchAsync(string id, TPatch patch)
        {
            var set = _context.Set<TDb>();
            var entity = await set.FindAsync(id);

            if (entity == null) return null;

            _mapper.Map(patch, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto?> UpdateAsync(string id, TUpdate update)
        {
            var set = _context.Set<TDb>();

            var entity = await set.FindAsync(id);

            if (entity == null) return null;

            _mapper.Map(update, entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public virtual Task BeforeInsert(TDb entity, TInsert insert)
        {
            return Task.CompletedTask;
        }
    }
}