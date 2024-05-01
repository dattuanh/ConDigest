using ConDigest.API.Models.Domain;

namespace ConDigest.API.Repositories
{
    public interface ICouponRepository
    {
        Task<List<Coupon>> GetAllCouponsAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageSize = 1, int pageNumber = 1000);

        Task<Coupon?> GetCouponByIdAsync(Guid id);

        Task<Coupon> CreateCouponAsync(Coupon coupon);

        Task<Coupon?> UpdateCouponAsync(Guid id ,Coupon coupon);

        Task<Coupon?> DeleteCouponAsync(Guid id);
    }
}
