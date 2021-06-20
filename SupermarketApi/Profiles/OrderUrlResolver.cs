namespace SupermarketApi.Profiles
{
    using System;
    using AutoMapper;
    using Microsoft.Extensions.Options;
    using SupermarketApi.Configuration;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities.OrderAggregate;

    internal sealed class OrderUrlResolver : IValueResolver<OrderItem, OrderItemDto, Uri>
    {
        private readonly ApiSettings apiSettings;

        public OrderUrlResolver(IOptions<ApiSettings> options)
        {
            this.apiSettings = options.Value;
        }

        public Uri Resolve(OrderItem source, OrderItemDto destination, Uri destMember, ResolutionContext context)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            _ = destination ?? throw new ArgumentNullException(nameof(destination));
            _ = context ?? throw new ArgumentNullException(nameof(context));

            return source.ItemOrdered.PicturePath != null ?
                new Uri($"{this.apiSettings.ApiUrl!}{source.ItemOrdered.PicturePath}") :
                throw new InvalidOperationException();
        }
    }
}
