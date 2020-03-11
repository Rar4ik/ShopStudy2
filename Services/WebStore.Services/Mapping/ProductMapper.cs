using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.Dto.Products;

namespace WebStore.Services.Mapping
{
    public static class ProductMapper
    {
        public static ProductDto ToDTO(this Domain.Entities.Product product) => product is null ? null : new ProductDto()
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand is null
                ? null
                : new BrandDto()
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name
                },
            Section = product.Section is null
                ? null
                : new SectionDto()
                {
                    Id = product.Section.Id,
                    Name = product.Section.Name
                }
        };

        public static IEnumerable<ProductDto> ToDTO(this IEnumerable<Domain.Entities.Product> product) =>
            product?.Select(ToDTO);
    }
}
