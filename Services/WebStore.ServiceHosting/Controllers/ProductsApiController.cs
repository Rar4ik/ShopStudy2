using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Dto;
using WebStore.Domain.Dto.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Products)] 
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductsApiController(IProductData productData)
        {
            _ProductData = productData;
        }
        /// <summary>Получение всех разделов каталога товаров</summary>
        /// <returns>Перечисление всех разделов каталога</returns>
        [HttpGet("sections")]
        public IEnumerable<Section> GetSections() => _ProductData.GetSections();

        /// <summary>Получение всех брендов товаров из каталога</summary>
        /// <returns>Перечисление брендов товаров каталога</returns>
        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands() => _ProductData.GetBrands();

        /// <summary>Получение товаров, удовлетворяющих критерию поиска</summary>
        /// <param name="Filter">Фильтр - критерий поиска товаров в каталоге</param>
        /// <returns>Перечисление всех товаров из каталога, удовлетворяющих критерию поиска</returns>
        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDto> GetProducts([FromBody] ProductFilter Filter = null) => _ProductData.GetProducts(Filter);

        /// <summary>Получение информации по товару, заданному идентификатором</summary>
        /// <param name="id">Идентификатор товара, информацию по которому требуется получить</param>
        /// <returns>Информацию по товару, заданному идентификатором</returns>
        [HttpGet("{id}"), ActionName("Get")]
        public ProductDto GetProductById(int id) => _ProductData.GetProductById(id);
    }
}
