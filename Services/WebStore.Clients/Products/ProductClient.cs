using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Dto;
using WebStore.Domain.Dto.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductClient : BaseClient, IProductData
    {

        public ProductClient(IConfiguration config) : base(config, WebApi.Products)
        {
        }

        public IEnumerable<SectionDto> GetSections() => Get<List<SectionDto>>($"{_ServiceAdress}/sections");
        public SectionDto GetSectionById(int id)=> Get<SectionDto>($"{_ServiceAdress}/sections/{id}");

        public IEnumerable<BrandDto> GetBrands() => Get<List<BrandDto>>($"{_ServiceAdress}/brands");
        public BrandDto GetBrandById(int id) => Get<BrandDto>($"{_ServiceAdress}/brands/{id}");

        public IEnumerable<ProductDto> GetProducts(ProductFilter Filter = null) =>
            Post(_ServiceAdress, Filter)
                .Content
                .ReadAsAsync<List<ProductDto>>()
                .Result;

        public ProductDto GetProductById(int id) => Get<ProductDto>($"{_ServiceAdress}/{id}");
    }
}
