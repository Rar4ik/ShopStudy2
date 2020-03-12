using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.Dto.Products;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class BrandMapper
    {
        public static BrandDto ToDTO(this Brand Brand) => Brand is null ? null : new BrandDto
        {
            Id = Brand.Id,
            Name = Brand.Name,
            Order = Brand.Order,
            ProductCount = Brand.Products?.Count ?? 0
        };

        public static Brand FromDTO(this BrandDto Brand) => Brand is null ? null : new Brand
        {
            Id = Brand.Id,
            Name = Brand.Name,
            Order = Brand.Order
        };

        public static IEnumerable<BrandDto> ToDTO(this IEnumerable<Brand> Brands) => Brands?.Select(ToDTO);

        public static IQueryable<BrandDto> TODTO(this IQueryable<Brand> Brands) => Brands.Select(
            Brand => new BrandDto
            {
                Id = Brand.Id,
                Name = Brand.Name,
                Order = Brand.Order,
                ProductCount = Brand.Products.Count()
            });
    }
}
