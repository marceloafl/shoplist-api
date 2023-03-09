using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Controllers;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Data;
using ShoplistAPI.Repository;
using ShoplistAPI.Profiles;
using FluentAssertions;

namespace ShoplistAPIxUnitTests
{
    public class ProductControllerUnitTest
    {
        private IUnitOfWork repository;
        private IMapper mapper;

        public static DbContextOptions<ShoplistContext> shoplistContextOptions { get; }

        // String de conexão do bando de dados para testes - ShoplistDBTest
        public static string stringConnection = "Data Source=NBQFC-8YHVYL3;Initial Catalog=ShoplistDBTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static ProductControllerUnitTest()
        {
            shoplistContextOptions = new DbContextOptionsBuilder<ShoplistContext>()
                .UseSqlServer(stringConnection)
                .Options;
        }

        public ProductControllerUnitTest()
        {

            // Configura o mapper usando o Profile
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
            });
            mapper = config.CreateMapper();

            // Configura o shoplistRepository usando o contexto (ShoplistContext)
            var context = new ShoplistContext(shoplistContextOptions);

            //DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();
            //db.Seed(context);

            repository = new UnitOfWork(context);
        }

        [Fact(DisplayName = "GET All Products - OkResult")]
        public async void GetProduct_Return_OkResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);

            //Act
            var data = await controller.GetAll();

            //Assert
            Assert.IsType<ActionResult<IQueryable<ProductDTO>>>(data);
        }

        [Fact(
            Skip = "Método GetAll no controller deve lançar exceção para este teste passar.",
            DisplayName = "GET All Products - BadRequestResult")]
        public async void GetProduct_Return_BadRequestResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);

            //Act
            var data = await controller.GetAll();

            //Assert
            Assert.IsType<BadRequestResult>(data.Result);
        }

        [Fact(DisplayName = "GET ProductById - OkResult")]
        public async void GetProductById_Return_OkResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);
            var productId = 1;

            // Act
            var data = await controller.GetById(productId);

            // Assert
            Assert.IsType<ActionResult<ProductDTO>>(data);
        }

        [Fact(DisplayName = "GET ProductById - NotFoundResult")]
        public async void GetProductById_ReturnNotFoundResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);
            var productId = 9999;

            // Act
            var data = await controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact(DisplayName = "POST Product - OkResult")]
        public async void PostProduct_ReturnOkResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);
            var newProduct = new ProductDTO()
            {
                Name = "Teste unitário - POST (Product)",
                Brand = "Teste unitário - POST (Product)",
                Description = "Teste unitário - POST (Product)",
                Number = 1,
                ShoplistId = 1

            };

            // Act
            var data = await controller.Add(newProduct);

            // Assert
            Assert.IsType<ActionResult<ProductDTO>>(data);
        }

        [Fact(
            Skip = "Método Add controller deve lançar exceção para este teste passar.",
            DisplayName = "POST Product - BadRequestResult")]
        public async void PostProduct_BadRequestResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);
            var newProduct = new ProductDTO()
            {
                Name = "Teste unitário - POST (Product)",
                Brand = "Teste unitário - POST (Product)",
                Description = "Teste unitário - POST (Product)",
                Number = 1,
                ShoplistId = 1

            };

            // Act
            var data = await controller.Add(newProduct);

            // Assert
            Assert.IsType<BadRequestResult>(data.Result);
        }

        [Fact(DisplayName = "PUT Product - OkResult")]
        public async void PutProduct_ReturnOkResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);
            var productId = 4;

            // Act
            var exinstingProduct = await controller.GetById(productId);
            var result = exinstingProduct.Should().BeAssignableTo<ActionResult<ProductDTO>>().Subject;

            var productDTO = new ProductDTO();
            productDTO.Id = productId;
            productDTO.Name = $"Teste unitário - PUT (Product) - Id modificado: {productId}";
            productDTO.Brand = $"Teste unitário - PUT (Product) - Id modificado: {productId}";
            productDTO.Description = $"Teste unitário - PUT (Product) - Id modificado: {productId}";
            productDTO.Number = 1 ;
            productDTO.ShoplistId = 1 ;

            var updatedData = await controller.Update(productId, productDTO);

            // Assert
            Assert.IsType<ActionResult<ProductDTO>>(updatedData);
        }

        [Fact(DisplayName = "PUT Product - NotFoundResult")]
        public async void PutProduct_ReturnNotFoundResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);
            var productId = 9999;

            // Act
            var data = await controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact(DisplayName = "DELETE Product - OkResult")]
        public async void DeleteProduct_ReturnOkResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);
            var productId = 12;

            // Act
            var data = await controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<OkObjectResult>(data.Result);
        }

        [Fact(DisplayName = "DELETE Product - NotFoundResult")]
        public async void DeleteProduct_ReturnNotFoundResult()
        {
            // Arrange
            var controller = new ProductController(repository, mapper);
            var productId = 9999;

            // Act
            var data = await controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(data.Result);
        }
    }
}
