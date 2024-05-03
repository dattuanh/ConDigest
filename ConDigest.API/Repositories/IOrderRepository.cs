using ConDigest.API.Models.Domain;

namespace ConDigest.API.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

        Task<Order?> GetByIdAsync(Guid id);

        Task<Order> CreateAsync(Order order, List<OrderDetail> orderDetail);

        Task<Order?> UpdateAsync(Guid id, Order order);

        Task<Order?> DeleteAsync(Guid orderId);
    }
}
