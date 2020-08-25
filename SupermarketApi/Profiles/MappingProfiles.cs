namespace SupermarketApi.Profiles
{
    using AutoMapper;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.Identity;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            _ = this.CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            _ = this.CreateMap<Address, AddressDto>()
                .ReverseMap();

            _ = this.CreateMap<CustomerBasketDto, CustomerBasket>();

            _ = this.CreateMap<BasketItemDto, BasketItem>();
        }
    }
}
