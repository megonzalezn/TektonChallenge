using Microsoft.AspNetCore.Mvc;
using Moq;
using Tekton.Model;
using Tekton.Services.Interface;
using Tekton.Services.Interfaces;

using TektonChallenge.Controllers;

namespace TektonTest.Controller
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> productService;
        private readonly Mock<ILogService> logService;
        public ProductControllerTest() 
        { 
            productService = new Mock<IProductService>();
            logService = new Mock<ILogService>();
        }  
        [Fact]
        public void Should_Return_All_Products()
        {
            //Arrange
            List<ProductResponseDTO> products = GetProducts();
            productService.Setup(x => x.GetAll()).Returns(products);
            var controller = new ProductController(productService.Object, logService.Object);

            //Act
            IActionResult result = controller.Get();

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            
            var returnValue = Assert.IsType<List<ProductResponseDTO>>(res.Value);

            Assert.Equal(products, returnValue);
        }

        [Fact]
        public async void Should_Return_Product_By_Id()
        {
            //Arrange
            List<ProductResponseDTO> products = GetProducts();
            productService.Setup(x => x.GetById(1)).ReturnsAsync(products[1]);
            var controller = new ProductController(productService.Object, logService.Object);

            //Act
            IActionResult result = await controller.GetById(1);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);

            var returnValue = Assert.IsType<ProductResponseDTO>(res.Value);

            Assert.Equal(products[1], returnValue);
        }

        [Fact]
        public async void Should_Create_Product()
        {
            //Arrange
            List<ProductResponseDTO> products = GetProducts();
            Mock<ProductRequestDTO> mockProduct = new Mock<ProductRequestDTO>(); 
            productService.Setup(x => x.Create(mockProduct.Object)).ReturnsAsync(products[1]);
            var controller = new ProductController(productService.Object, logService.Object);

            //Act
            IActionResult result = await controller.Post(mockProduct.Object);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);

            var returnValue = Assert.IsType<ProductResponseDTO>(res.Value);

            Assert.Equal(products[1], returnValue);
        }

        [Fact]
        public async void Should_Have_Invalid_ModelState_at_Create_Product()
        {
            //Arrange
            List<ProductResponseDTO> products = GetProducts();
            Mock<ProductRequestDTO> mockProduct = new Mock<ProductRequestDTO>();
            productService.Setup(x => x.Create(mockProduct.Object)).ReturnsAsync(products[1]);
            var controller = new ProductController(productService.Object, logService.Object);
            controller.ModelState.AddModelError("","");
            //Act
            IActionResult result = await controller.Post(mockProduct.Object);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Should_Update_Product()
        {
            //Arrange
            List<ProductResponseDTO> products = GetProducts();
            Mock<ProductRequestDTO> mockProduct = new Mock<ProductRequestDTO>();
            productService.Setup(x => x.Update(mockProduct.Object)).ReturnsAsync(products[1]);
            var controller = new ProductController(productService.Object, logService.Object);

            //Act
            IActionResult result = await controller.Put(mockProduct.Object.Id, mockProduct.Object);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);

            var returnValue = Assert.IsType<ProductResponseDTO>(res.Value);

            Assert.Equal(products[1], returnValue);
        }

        [Fact]
        public async void Should_Have_Invalid_ModelState_at_Update_Product()
        {
            //Arrange
            List<ProductResponseDTO> products = GetProducts();
            Mock<ProductRequestDTO> mockProduct = new Mock<ProductRequestDTO>();
            productService.Setup(x => x.Update(mockProduct.Object)).ReturnsAsync(products[1]);
            var controller = new ProductController(productService.Object, logService.Object);
            controller.ModelState.AddModelError("", "");

            //Act
            IActionResult result = await controller.Put(mockProduct.Object.Id, mockProduct.Object);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Should_Have_Different_Ids_at_Update_Product()
        {
            //Arrange
            List<ProductResponseDTO> products = GetProducts();
            ProductRequestDTO product = new ProductRequestDTO{ Id = 1};
            
            var controller = new ProductController(productService.Object, logService.Object);
            
            //Act
            IActionResult result = await controller.Put(2, product);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<BadRequestObjectResult>(result);
        }

        private List<ProductResponseDTO> GetProducts()
        {
            return new List<ProductResponseDTO>()
            {
                new ProductResponseDTO(){Id = 1, Name="Product 1", Description="Awesome product", Price= 100, Status= "Activo", Discount= 10, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new ProductResponseDTO(){Id = 2, Name="Product 2", Description="Awesome product", Price= 150, Status= "Inactivo", Discount= 15, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new ProductResponseDTO(){Id = 3, Name="Product 3", Description="Awesome product", Price= 200, Status= "Activo", Discount= 10, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new ProductResponseDTO(){Id = 4, Name="Product 4", Description="Awesome product", Price= 250, Status= "Inactivo", Discount= 5, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new ProductResponseDTO(){Id = 5, Name="Product 5", Description="Awesome product", Price= 300, Status= "Activo", Discount= 10, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new ProductResponseDTO(){Id = 6, Name="Product 6", Description="Awesome product", Price= 500, Status= "Inactivo", Discount= 20, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 }
            };
        }
    }
}