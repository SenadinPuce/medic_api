using Domain.Dtos.Search;

namespace Domain.Interfaces
{
    public interface IReadService<TDto, TSearch> where TDto : class where TSearch : class
    {
        Task<List<TDto>> GetAllAsync(TSearch search);
        Task<TDto?> GetByIdAsync(string id);
    }
}