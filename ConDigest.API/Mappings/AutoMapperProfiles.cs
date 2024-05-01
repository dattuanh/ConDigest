using AutoMapper;
using ConDigest.API.Models.Domain;
using ConDigest.API.Models.DTO.CouponDTOs;

namespace ConDigest.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
            CreateMap<AddCouponRequestDto, Coupon>().ReverseMap();
            CreateMap<UpdateCouponRequestDto, Coupon>().ReverseMap();
        }
    }
}
