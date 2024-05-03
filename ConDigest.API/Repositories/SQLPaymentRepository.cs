using ConDigest.API.Data;
using ConDigest.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConDigest.API.Repositories
{
    public class SQLPaymentRepository : IPaymentRepository
    {
        private readonly ConDigestDBContext dBContext;

        public SQLPaymentRepository(ConDigestDBContext _dBContext)
        {
            this.dBContext = _dBContext;
        }

        public async Task<Payment> CreateAsync(Payment payment)
        {
            await dBContext.Payments.AddAsync(payment);
            await dBContext.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> DeleteAsync(Guid id)
        {
            var existingPayment = await dBContext.Payments.FirstOrDefaultAsync(x => x.Id == id);

            if (existingPayment == null)
            {
                return null;
            }

            dBContext.Payments.Remove(existingPayment);
            await dBContext.SaveChangesAsync();
            return existingPayment;
        }

        public async Task<List<Payment>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var payments = dBContext.Payments.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("PaymentName", StringComparison.OrdinalIgnoreCase))
                {
                    payments = payments.Where(x => x.PaymentName.Contains(filterQuery));
                }
            }

            // Sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("PaymentName", StringComparison.OrdinalIgnoreCase))
                {
                    payments = isAscending ? payments.OrderBy(x => x.PaymentName) : payments.OrderByDescending(x => x.PaymentName);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await payments.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await dBContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Payment?> UpdateAsync(Guid id, Payment payment)
        {
            var existingPayment = await dBContext.Payments.FirstOrDefaultAsync(x => x.Id == id);

            if (existingPayment == null)
            {
                return null;
            }

            existingPayment.PaymentName = payment.PaymentName;
            existingPayment.PaymentWay = payment.PaymentWay;
            existingPayment.PaymentMessage = payment.PaymentMessage;
            existingPayment.OrderId = payment.OrderId;

            await dBContext.SaveChangesAsync();
            return existingPayment;
        }
    }
}
