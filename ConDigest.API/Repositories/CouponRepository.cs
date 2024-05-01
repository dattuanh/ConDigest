using ConDigest.API.Data;
using ConDigest.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConDigest.API.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ConDigestDBContext _context;
        public CouponRepository(ConDigestDBContext context)
        {
            _context = context;
        }

        public async Task<Coupon> CreateCouponAsync(Coupon coupon)
        {
            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();
            return coupon;
        }

        public async Task<Coupon?> DeleteCouponAsync(Guid id)
        {
            var existingCoupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCoupon == null)
            {
                return null;
            }

            _context.Coupons.Remove(existingCoupon);
            await _context.SaveChangesAsync();
            return existingCoupon;
        }

        public async Task<List<Coupon>> GetAllCouponsAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageSize = 1, int pageNumber = 1000)
        {
            var coupons = _context.Coupons.AsQueryable();

            // Filter
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    coupons = coupons.Where(x => x.CouponName.Contains(filterQuery));
                }
                if (filterOn.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    coupons = coupons.Where(x => x.CouponCode.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    coupons = isAscending ? coupons.OrderBy(x => x.CouponName) : coupons.OrderByDescending(x => x.CouponName);
                }
                if (sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    coupons = isAscending ? coupons.OrderBy(x => x.CouponCode) : coupons.OrderByDescending(x => x.CouponCode);
                }
                if (sortBy.Equals("FromDate", StringComparison.OrdinalIgnoreCase))
                {
                    coupons = isAscending ? coupons.OrderBy(x => x.FromDate) : coupons.OrderByDescending(x => x.FromDate);
                }
                if (sortBy.Equals("ToDate", StringComparison.OrdinalIgnoreCase))
                {
                    coupons = isAscending ? coupons.OrderBy(x => x.ToDate) : coupons.OrderByDescending(x => x.ToDate);
                }
            }

            //Pagination
            var skipNumber = (pageNumber - 1) * pageSize;
            coupons = coupons.Skip(skipNumber).Take(pageSize);

            return await coupons.ToListAsync();
        }

        public async Task<Coupon?> GetCouponByIdAsync(Guid id)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);           
        }

        public async Task<Coupon?> UpdateCouponAsync(Guid id, Coupon coupon)
        {
            var existingCoupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);

            if(existingCoupon == null)
            {
                return null;
            }
            existingCoupon.CouponCode = coupon.CouponCode;
            existingCoupon.CouponName = coupon.CouponName;
            existingCoupon.FromDate = coupon.FromDate;
            existingCoupon.ToDate = coupon.ToDate;

            await _context.SaveChangesAsync();
            
            return existingCoupon;
        }
    }
}
