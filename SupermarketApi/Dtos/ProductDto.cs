﻿#nullable disable
namespace SupermarketApi.Dtos
{
    using System;

    public class ProductDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public Uri PictureUrl { get; set; }

        public decimal Price { get; set; }

        public string ProductBrand { get; set; }

        public string ProductType { get; set; }
    }
}
