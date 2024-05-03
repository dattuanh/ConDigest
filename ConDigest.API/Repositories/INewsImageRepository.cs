using ConDigest.API.Models.Domain;

namespace ConDigest.API.Repositories
{
    public interface INewsImageRepository
    {
        Task<NewsImage> UploadNewImage(NewsImage image);
    }
}
