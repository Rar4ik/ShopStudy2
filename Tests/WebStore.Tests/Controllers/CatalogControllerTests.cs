using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.Dto.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using Xunit.Sdk;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void Details_Returned_With_Correct_View()
        {
            const int expected_product_id = 1;
            const decimal expected_price = 10m;

            var expected_name = $"Product id{expected_product_id}";
            var expected_brand_name = $"Brand of product{expected_product_id}";

            var product_data_mock = new Mock<IProductData>();
            product_data_mock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDto()
                {
                    Id = id,
                    Name = $"Product id{id}",
                    ImageUrl = $"Image_id_{id}.png",
                    Order = 1,
                    Price = expected_price,
                    Brand = new BrandDto()
                    {
                        Id = 1,
                        Name = $"Brand of product{id}"
                    },
                    Section = new SectionDto()
                    {
                        Id = 1,
                        Name = $"Section of product{id}"
                    }
                });

            var controller = new CatalogController(product_data_mock.Object);

            var result = controller.Details(expected_product_id);

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

            Assert.Equal(expected_product_id, model.Id);
            Assert.Equal(expected_price, model.Price); 
            Assert.Equal(expected_name, model.Name);
            Assert.Equal(expected_brand_name, model.Brand);
        }

        [TestMethod]
        public void Details_Returns_NotFound_if_Product_does_not_Exist()
        {
            const int expected_product_id = 1;

            var product_data_mock = new Mock<IProductData>();
            product_data_mock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns(default(ProductDto));

            var controller = new CatalogController(product_data_mock.Object);
            var result = controller.Details(expected_product_id);
            Assert.IsType<NotFoundResult>(result);
        }

        [TestMethod]
        public void Shop_Returns_Correct_View()
        {
            const int expected_brand_id = 1;
            const int expected_section_id = 1;
            var product_data_mock = new Mock<IProductData>();
            product_data_mock
                .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
                .Returns<ProductFilter>(filter => new[]
                {
                    new ProductDto
                    {
                        Id = 1,
                        Name = "Product1",
                        Order = 1,
                        Price = 10m,
                        ImageUrl = "Product1.png",
                        Brand = new BrandDto()
                        {
                            Id = 1,
                            Name = "ProductBrand"
                        },
                        Section = new SectionDto()
                        {
                            Id = 1,
                            Name = "SectionName"
                        }
                    },
                    new ProductDto
                    {
                        Id = 2,
                        Name = "Product2",
                        Order = 2,
                        Price = 10m,
                        ImageUrl = "Product2.png",
                        Brand = new BrandDto()
                        {
                            Id = 2,
                            Name = "ProductBrand2"
                        },
                        Section = new SectionDto()
                        {
                            Id = 2,
                            Name = "SectionName2"
                        }
                    }
                });
            var mapper_mock = new Mock<IMapper>();
            mapper_mock
                .Setup(m => m.Map<ProductViewModel>(It.IsAny<ProductDto>()))
                .Returns<ProductDto>(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Brand = p.Brand.Name,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price
                });

            var controller = new CatalogController(product_data_mock.Object);

            var result = controller.Shop(expected_section_id, expected_brand_id, mapper_mock.Object);

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<CatalogViewModel>(view_result.ViewData.Model);

            Assert.Equal(2, model.Products.Count());
            Assert.Equal(expected_section_id, model.SectionId);
            Assert.Equal(expected_brand_id, model.BrandId);

        }
    }
}
