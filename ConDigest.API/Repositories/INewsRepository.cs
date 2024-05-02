using ConDigest.API.Models.Domain;

namespace ConDigest.API.Repositories
{
    public interface INewsRepository
    {
        Task<List<News>> GetAllNewsAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageSize = 1, int pageNumber = 1000);

        Task<News?> GetNewsByIdAsync(Guid id);

        Task<News> CreateNewsAsync(News news);

        Task<News?> UpdateNewsAsync(Guid id, News news);

        Task<News?> DeleteNewsAsync(Guid id);
    }
}
