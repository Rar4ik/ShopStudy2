using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Dto;
using WebStore.Domain.Dto.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Product
{
    //
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _db;

        public SqlProductData(WebStoreContext db) => _db = db;

        public IEnumerable<SectionDto> GetSections() => _db.Sections.TODTO().AsEnumerable();

        public SectionDto GetSectionById(int id) => _db.Sections.Find(id).ToDTO();

        public IEnumerable<BrandDto> GetBrands() => _db.Brands.TODTO().AsEnumerable();

        public BrandDto GetBrandById(int id) => _db.Brands.Find(id).ToDTO();

        public IEnumerable<ProductDto> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Domain.Entities.Product> query = _db.Products;

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.Ids?.Count > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));

            return query.AsEnumerable().ToDTO();
        }

        public ProductDto GetProductById(int id)
        { 
            var product = _db.Products
           .Include(p => p.Brand)
           .Include(p => p.Section)
           .FirstOrDefault(p => p.Id == id);
            return product.ToDTO();
        } 
    }
}
