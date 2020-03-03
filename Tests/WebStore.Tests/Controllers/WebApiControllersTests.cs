using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces.Api;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class WebApiControllersTests
    {
        [TestMethod]
        public void Index_Return_View_with_Values()
        {
            var expected_items = new[] {"1", "2", "3"};

            var value_service_mock = new Mock<IValueService>();
            value_service_mock
                .Setup(service => service.Get())
                .Returns(expected_items);

            var controller = new WebApiTestController(value_service_mock.Object);

            var result = controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result);

            Assert.Equal(expected_items.Length, model.Count());

            value_service_mock.Verify(service=>service.Get());
            value_service_mock.VerifyNoOtherCalls();
        }
    }
}
