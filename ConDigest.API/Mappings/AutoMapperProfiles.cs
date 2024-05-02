using AutoMapper;
using ConDigest.API.Models.Domain;
using ConDigest.API.Models.DTO.CouponDTOs;
using ConDigest.API.Models.DTO.NewsDTOs;

namespace ConDigest.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
            CreateMap<AddCouponRequestDto, Coupon>().ReverseMap();
            CreateMap<UpdateCouponRequestDto, Coupon>().ReverseMap();

            CreateMap<News, NewsDto>().ReverseMap();
            CreateMap<AddNewsRequestDto, News>().ReverseMap();
            CreateMap<UpdateNewsRequestDto, News>().ReverseMap();
        }
    }
}
