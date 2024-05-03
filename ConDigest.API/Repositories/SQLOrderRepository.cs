using ConDigest.API.Data;
using ConDigest.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConDigest.API.Repositories
{
    public class SQLOrderRepository : IOrderRepository
    {
        private readonly ConDigestDBContext dbContext;

        public SQLOrderRepository(ConDigestDBContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<Order> CreateAsync(Order order, List<OrderDetail> orderDetail)
        {
            var newOrder = await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            foreach (var item in orderDetail)
            {
                item.OrderId = newOrder.Entity.Id;
                var product = await dbContext.ProductItems.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                item.ProductItem = product;
                item.ProductId = product.Id;
                await dbContext.OrderDetails.AddAsync(item);
            }
            await dbContext.SaveChangesAsync();

            return newOrder.Entity;
        }

        public async Task<Order?> DeleteAsync(Guid orderId)
        {
            var existingOrder = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (existingOrder == null)
            {
                return null;
            }

            var existingOrderItemByOrderID = await dbContext.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();

            if (existingOrderItemByOrderID.Count > 0)
            {
                dbContext.OrderDetails.RemoveRange(existingOrderItemByOrderID);
                await dbContext.SaveChangesAsync();
            } 

            dbContext.Orders.Remove(existingOrder);
            await dbContext.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<List<Order>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var orders = dbContext.Orders.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    orders = orders.Where(x => x.Status.Contains(filterQuery));
                }
            }

            // Sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("TotalAmount", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(x => x.TotalAmount) : orders.OrderByDescending(x => x.TotalAmount);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await orders.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Order?> UpdateAsync(Guid id, Order order)
        {
            throw new NotImplementedException();
        }

    }
}
