using Microsoft.EntityFrameworkCore;
using Moq;
using Tekton.Entity;
using Tekton.Repository;


namespace TektonTest.Repository
{
    public class ProductRepositoryTest
    {
        private readonly Mock<Context> context;
        public ProductRepositoryTest()
        {
            var products = GetProducts();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(products.AsQueryable().Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(products.AsQueryable().Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(products.AsQueryable().ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(products.AsQueryable().GetEnumerator());

            context = new Mock<Context>();
            context.Setup(x => x.Product).Returns(mockSet.Object);

        }

        [Fact]
        public void Should_Return_All_Products()
        {
            //Arrange
            List<Product> products = GetProducts();
            var repository = new ProductRepository(context.Object);

            //Act
            List<Product> result = repository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(products.Count, result.Count);
        }

        [Fact]
        public void Should_Return_Product_By_Id()
        {
            //Arrange
            List<Product> products = GetProducts();
            var repository = new ProductRepository(context.Object);

            //Act
            Product result = repository.GetById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(products[0].Id, result.Id);
        }

        [Fact]
        public void Should_Create_Product()
        {
            //Arrange
            List<Product> products = GetProducts();
            var repository = new ProductRepository(context.Object);

            //Act
            Product result = repository.Create(products[0]);

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
