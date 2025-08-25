using AutoMapper;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Domain.Entity;

namespace PacaGroup.Ecommerce.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            // Add your mappings here
            // CreateMap<Source, Destination>();
            // Example: CreateMap<ProductDto, ProductEntity>();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerDto2>().ReverseMap();

            // El que falta:
            CreateMap<User, UserDto>().ReverseMap();

            // Si los casos de uso son diferentes, puedes usar .ForMember para mapear propiedades específicas
            //CreateMap<Customers, CustomersDto>().ReverseMap()
            //    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            //    .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
            //    .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactName))
            //    .ForMember(dest => dest.ContactTitle, opt => opt.MapFrom(src => src.ContactTitle))
            //    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            //    .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
            //    .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            //    .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            //    .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            //    .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax));
        }
    }
}
