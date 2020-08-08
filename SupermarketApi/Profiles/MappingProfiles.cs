namespace SupermarketApi.Profiles
{
    using AutoMapper;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            _ = this.CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name));
        }
    }
}
