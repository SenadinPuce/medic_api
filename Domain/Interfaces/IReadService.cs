using Domain.Dtos;

namespace Domain.Interfaces
{
    public interface IReadService<TDto, TSearch> where TDto : class where TSearch : class
    {
        Task<PagedResult<TDto>> GetAllAsync(TSearch search);
        Task<TDto?> GetByIdAsync(string id);
    }
}