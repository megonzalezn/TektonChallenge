using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Tekton.Entity;
using Tekton.Model;
using Tekton.Repository.Interfaces;
using Tekton.Services;

namespace TektonTest.Service
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> repository;
        private readonly Mock<IAppCache> cache;
        private Dictionary<int, string> statusList;
        public ProductServiceTest()
        {
            repository = new Mock<IProductRepository>();
            cache = new Mock<IAppCache>();
            statusList = new Dictionary<int, string>() {
                    {0, "Inactivo"},
                    {1, "Activo"}
                };
            cache.Setup(x => x.GetOrAdd(It.IsAny<string>(), It.IsAny<Func<ICacheEntry, Dictionary<int, string>>>(), It.IsAny<MemoryCacheEntryOptions>())).Returns(statusList);
        }

        [Fact]
        public void Should_Return_All_Products()
        {
            //Arrange
            List<Product> products = GetProducts();
            repository.Setup(x => x.GetAll()).Returns(products);
            var service = new ProductService(repository.Object, cache.Object);

            //Act
            List<ProductResponseDTO> result = service.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(products.Count, result.Count);

        }

        [Fact]
        public async void Should_Return_Product_By_Id()
        {
            //Arrange
            List<Product> products = GetProducts();
            repository.Setup(x => x.GetById(1)).Returns(products[0]);
            var service = new ProductService(repository.Object, cache.Object);

            //Act
            ProductResponseDTO result = await service.GetById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(products[0].Id, result.Id);

        }

        [Fact]
        public async void Should_Create_Product()
        {
            //Arrange
            List<Product> products = GetProducts();
            
            repository.Setup(x => x.Create(It.IsAny<Product>())).Returns(products[0]);
            var service = new ProductService(repository.Object, cache.Object);

            //Act
            ProductResponseDTO result = await service.Create(new Mock<ProductRequestDTO>().Object);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(products[0].Id, result.Id);
        }

        [Fact]
        public async void Should_Update_Product()
        {
            //Arrange
            List<Product> products = GetProducts();

            repository.Setup(x => x.Update(It.IsAny<Product>())).Returns(products[0]);
            var service = new ProductService(repository.Object, cache.Object);

            //Act
            ProductResponseDTO result = await service.Update(new Mock<ProductRequestDTO>().Object);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(products[0].Id, result.Id);
        }

        private List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product(){Id = 1, Name="Product 1", Description="Awesome product", Price= 100, Status= 1, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new Product(){Id = 2, Name="Product 2", Description="Awesome product", Price= 150, Status= 0, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new Product(){Id = 3, Name="Product 3", Description="Awesome product", Price= 200, Status= 1, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new Product(){Id = 4, Name="Product 4", Description="Awesome product", Price= 250, Status= 0, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new Product(){Id = 5, Name="Product 5", Description="Awesome product", Price= 300, Status= 1, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 },
                new Product(){Id = 6, Name="Product 6", Description="Awesome product", Price= 500, Status= 0, Creation=DateTime.Now.AddDays(-5), LastUpdated = DateTime.Now.AddDays(-3), Stock=150 }
            };
        }

    }
}
