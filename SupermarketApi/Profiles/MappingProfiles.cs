namespace SupermarketApi.Profiles
{
    using AutoMapper;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.OrderAggregate;
    using IdentityAdress = Entities.Identity.Address;
    using OrderAdress = Entities.OrderAggregate.Address;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            _ = this.CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            _ = this.CreateMap<IdentityAdress, AddressDto>()
                .ReverseMap();

            _ = this.CreateMap<AddressDto, OrderAdress>();

            _ = this.CreateMap<CustomerBasketDto, CustomerBasket>();

            _ = this.CreateMap<BasketItemDto, BasketItem>();

            _ = this.CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

            _ = this.CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderUrlResolver>());


        }
    }
}
