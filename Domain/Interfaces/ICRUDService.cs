namespace Domain.Interfaces
{
    public interface ICRUDService<TDto, TSearch, TInsert, TUpdate, TPatch>
        : IReadService<TDto, TSearch>
        where TDto : class
        where TSearch : class
        where TInsert : class
        where TUpdate : class
        where TPatch : class
    {
        Task<TDto?> InsertAsync(TInsert insert);
        Task<TDto?> UpdateAsync(string id, TUpdate update);
        Task<TDto?> PatchAsync(string id, TPatch patch);
    }
}