﻿#nullable disable
namespace SupermarketApi.Entities
{
    public sealed class Product : BaseEntity
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public string PicturePath { get; set; }

        public decimal Price { get; set; }

        public ProductBrand ProductBrand { get; set; }

        public int? ProductBrandId { get; set; }

        public ProductType ProductType { get; set; }

        public int? ProductTypeId { get; set; }
    }
}
