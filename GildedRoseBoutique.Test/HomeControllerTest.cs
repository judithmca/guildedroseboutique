using GildedRoseOnlineStore.Models;
using GildedRoseOnlineStore.Data.MockData;
using Moq;
using System;
using Xunit;
using GildedRoseOnlineStore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Castle.Core.Logging;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseBoutique.Test
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexUnitTest()
        {
            //arrange
            var mockRepoProduct = new Mock<IProductRepository>();
            var mockRepoOrder = new Mock<IOrderRepository>();
            var mockRepoEmail = new Mock<IEmailSender>();


            mockRepoProduct.Setup(n => n.GetAll()).Returns(MockData.GetTestProductItems());
            var controller = new HomeController(mockRepoProduct.Object, mockRepoOrder.Object, mockRepoEmail.Object);

            //act
            var result = controller.Index();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultProducts = Assert.IsAssignableFrom<List<Product>>(viewResult.ViewData.Model);
            Assert.Equal(5, viewResultProducts.Count);
        }

        [Theory]
        [InlineData(1, 6)]
        public void DetailsUnitTest(int validId, int invalidId)
        {
            //arrange
            var mockRepoProduct = new Mock<IProductRepository>();
            var mockRepoOrder = new Mock<IOrderRepository>();
            var mockRepoEmail = new Mock<IEmailSender>();

            mockRepoProduct.Setup(n => n.GetProductById(validId)).Returns(MockData.GetTestProductItems().FirstOrDefault(x => x.Id == validId));
            var controller = new HomeController(mockRepoProduct.Object, mockRepoOrder.Object, mockRepoEmail.Object);

            //act
            var result = controller.Details(validId);

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultValue = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
            Assert.Equal("Product 1", viewResultValue.Name);
            Assert.Equal("Testing Product 1", viewResultValue.Description);
            Assert.Equal(10, viewResultValue.Quantity);
            Assert.Equal(500, viewResultValue.Price);
            Assert.Equal("1.jpg", viewResultValue.Picture);

            //arrange
            mockRepoProduct.Setup(n => n.GetProductById(invalidId)).Returns(MockData.GetTestProductItems().FirstOrDefault(x => x.Id == invalidId));

            //act
            var notFoundResult = controller.Details(invalidId);

            //assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void CreateTest()
        {
            //arrange
            var mockRepoProduct = new Mock<IProductRepository>();
            var mockRepoOrder = new Mock<IOrderRepository>();
            var mockRepoEmail = new Mock<IEmailSender>();

            var controller = new HomeController(mockRepoProduct.Object, mockRepoOrder.Object, mockRepoEmail.Object);
            var newValidItem = new Product()
            {
                Id=7,
                Name = "Product 7",
                Description = "Description 7",
                Picture = "7.jpg",
                Quantity = 77,
                Price = 57
            };

            //act
            var result = controller.Create(newValidItem);

            //assert
            var rediretToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", rediretToActionResult.ActionName);
            Assert.Null(rediretToActionResult.ControllerName);

            //arrange
            var newInvalidItem = new Product()
            {
                Id = 8,
                Name = "Product 8",
                Description = "Description 7",
            };
            controller.ModelState.AddModelError("Price", "The price is required");

            //act
            var resultInvalid = controller.Create(newInvalidItem);

            //assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultInvalid);
           // Assert.IsType<SerializableError>(badRequestResult);
        }

    }
}
