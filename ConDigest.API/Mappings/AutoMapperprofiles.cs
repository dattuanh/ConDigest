using AutoMapper;
using ConDigest.API.Models.Domain;
using ConDigest.API.Models.DTO;

namespace ConDigest.API.Mappings
{
    public class AutoMapperprofiles : Profile
    {
        public AutoMapperprofiles()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, AddOrderRequestDto>().ReverseMap();
            CreateMap<OrderDetail, AddOrderItemRequestDto>().ReverseMap();

            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Payment, AddPaymentRequestDto>().ReverseMap();
            CreateMap<Payment, UpdatePaymentRequestDto>().ReverseMap();
        }
    }
}
