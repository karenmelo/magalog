using AutoMapper;
using Magalog.Application.Dtos;
using Magalog.Domain.Entitites;

namespace Magalog.Application.AutoMapper;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Products, src => src.MapFrom(s => s.OrderItems))
            .ForMember(dest => dest.Order_Id, src => src.MapFrom(s => s.Order_id))
            .ForMember(dest => dest.Date, src => src.MapFrom(s => s.Date))
            .ForMember(dest => dest.Total, src => src.MapFrom(s => s.Total))
            .ReverseMap();

        CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.Product_id, src => src.MapFrom(s => s.Product_id))
                .ForMember(dest => dest.Value, src => src.MapFrom(s => s.Value))
                .ReverseMap();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Orders, src => src.MapFrom(s => s.Orders))
            .ReverseMap();
    }

}
