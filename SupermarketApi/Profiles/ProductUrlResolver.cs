namespace SupermarketApi.Profiles
{
    using System;
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities;

    public class ProductUrlResolver : IValueResolver<Product, ProductDto, Uri>
    {
        private readonly IConfiguration configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Uri Resolve(Product source, ProductDto destination, Uri destMember, ResolutionContext context)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            _ = destination ?? throw new ArgumentNullException(nameof(destination));
            _ = context ?? throw new ArgumentNullException(nameof(context));

            return source.PicturePath != null ?
                new Uri($"{this.configuration["ApiUrl"]}{source.PicturePath}") :
                throw new InvalidOperationException();
        }
    }
}
