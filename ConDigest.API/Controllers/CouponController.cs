using AutoMapper;
using ConDigest.API.Data;
using ConDigest.API.Models.Domain;
using ConDigest.API.Models.DTO.CouponDTOs;
using ConDigest.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConDigest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ConDigestDBContext _context;
        public readonly IMapper _mapper;
        public CouponController(ICouponRepository couponRepository, IMapper mapper, ConDigestDBContext conDigestDBContext)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
            _context = conDigestDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageSize = 1000, [FromQuery] int pageNumber = 1)
        {
            var coupons = await _couponRepository.GetAllCouponsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageSize, pageNumber);
            //var coupon = _context.Coupons.ToList();
            return Ok(_mapper.Map<List<CouponDto>>(coupons));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCouponById([FromRoute] Guid id)
        {
            var coupon = await _couponRepository.GetCouponByIdAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CouponDto>(coupon));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody] AddCouponRequestDto coupon)
        {
            var couponDomain = _mapper.Map<Coupon>(coupon);

            await _couponRepository.CreateCouponAsync(couponDomain);

            var displayCoupon = _mapper.Map<CouponDto>(couponDomain);

            return CreatedAtAction(nameof(GetCouponById), new { id = displayCoupon.Id }, displayCoupon);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCoupon([FromRoute] Guid id, [FromBody] UpdateCouponRequestDto coupon)
        {
            var couponDomain = _mapper.Map<Coupon>(coupon);
            var updatedCoupon = await _couponRepository.UpdateCouponAsync(id, couponDomain);
            if (updatedCoupon == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CouponDto>(updatedCoupon));
        } 

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCoupon(Guid id)
        {
            var coupon = await _couponRepository.DeleteCouponAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CouponDto>(coupon));
        }        
    }
}
