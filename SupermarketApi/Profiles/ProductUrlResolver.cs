namespace SupermarketApi.Profiles
{
    using System;
    using AutoMapper;
    using Microsoft.Extensions.Options;
    using SupermarketApi.Configuration;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities;

    public class ProductUrlResolver : IValueResolver<Product, ProductDto, Uri>
    {
        private readonly ApiSettings apiSettings;

        public ProductUrlResolver(IOptions<ApiSettings> options)
        {
            this.apiSettings = options.Value;
        }

        public Uri Resolve(Product source, ProductDto destination, Uri destMember, ResolutionContext context)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            _ = destination ?? throw new ArgumentNullException(nameof(destination));
            _ = context ?? throw new ArgumentNullException(nameof(context));

            return source.PicturePath != null ?
                new Uri($"{this.apiSettings.ApiUrl!}{source.PicturePath}") :
                throw new InvalidOperationException();
        }
    }
}
