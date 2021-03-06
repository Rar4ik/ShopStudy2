﻿using System.Collections.Generic;
using WebStore.Domain.Dto;
using WebStore.Domain.Dto.Products;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDto> GetSections();

        SectionDto GetSectionById(int id);

        IEnumerable<BrandDto> GetBrands();

        BrandDto GetBrandById(int id);

        IEnumerable<ProductDto> GetProducts(ProductFilter Filter = null);

        ProductDto GetProductById(int id);
    }
}
