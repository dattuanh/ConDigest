using ConDigest.API.Data;
using ConDigest.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConDigest.API.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly ConDigestDBContext _context;
        public NewsRepository(ConDigestDBContext context)
        {
            _context = context;
        }
        public async Task<News> CreateNewsAsync(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<News?> DeleteNewsAsync(Guid id)
        {
            var existingNews = await _context.News.FirstOrDefaultAsync(n => n.Id == id);
            if (existingNews == null)
            {
                return null;
            }
            
            _context.News.Remove(existingNews);
            await _context.SaveChangesAsync();
            return existingNews;
        }

        public async Task<List<News>> GetAllNewsAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageSize = 1000, int pageNumber = 1)
        {
            var news = _context.News.AsQueryable();
            //filter
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    news = news.Where(x => x.Title.Contains(filterQuery));
                }
                if (filterOn.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    news = news.Where(x => x.CreatedBy.Contains(filterQuery));
                }
            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    news = isAscending ? news.OrderBy(x => x.Title) : news.OrderByDescending(x => x.Title);
                }
                if (sortBy.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    news = isAscending ? news.OrderBy(x => x.CreatedBy) : news.OrderByDescending(x => x.CreatedBy);
                }
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    news = isAscending ? news.OrderBy(x => x.ModifiedDate) : news.OrderByDescending(x => x.ModifiedDate);
                }
            }

            //pagination
            var skipNumber = (pageNumber - 1) * pageSize;
            news = news.Skip(skipNumber).Take(pageSize);
            return await news.ToListAsync();
        }

        public async Task<News?> GetNewsByIdAsync(Guid id)
        {
            return await _context.News.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<News?> UpdateNewsAsync(Guid id, News news)
        {
            var existingNews = await _context.News.FirstOrDefaultAsync(n => n.Id == id);
            if (existingNews == null)
            {
                return null;
            }

            existingNews.Title = news.Title;
            existingNews.Content = news.Content;
            existingNews.CreatedBy = news.CreatedBy;
            existingNews.ModifiedBy = news.ModifiedBy;
            existingNews.ModifiedDate = news.ModifiedDate;
                
            await _context.SaveChangesAsync();
            return existingNews;
        }
    }
}
