using ConDigest.API.Models.Domain;

namespace ConDigest.API.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

        Task<Payment?> GetByIdAsync(Guid id);

        Task<Payment> CreateAsync(Payment payment);

        Task<Payment?> UpdateAsync(Guid id, Payment payment);

        Task<Payment?> DeleteAsync(Guid id);
    }
}
