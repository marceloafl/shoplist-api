using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Controllers;
using ShoplistAPI.Data;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Pagination;
using ShoplistAPI.Profiles;
using ShoplistAPI.Repository;

namespace ShoplistAPIxUnitTests
{
    public class ShoplistControllerUnitTest
    {
        private IUnitOfWork repository;
        private IMapper mapper;

        public static DbContextOptions<ShoplistContext> shoplistContextOptions { get; }

        // String de conexão do bando de dados para testes - ShoplistDBTest
        public static string stringConnection = "Data Source=NBQFC-8YHVYL3;Initial Catalog=ShoplistDBTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static ShoplistControllerUnitTest()
        {
            shoplistContextOptions = new DbContextOptionsBuilder<ShoplistContext>()
                .UseSqlServer(stringConnection)
                .Options;
        }

        public ShoplistControllerUnitTest()
        {

            // Configura o mapper usando o Profile
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ShoplistProfile());
                cfg.AddProfile(new ProductProfile());
            });
            mapper = config.CreateMapper();

            // Configura o shoplistRepository usando o contexto (ShoplistContext)
            var context = new ShoplistContext(shoplistContextOptions);

            DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();
            //db.Seed(context);

            repository = new UnitOfWork(context);
        }

        [Fact(DisplayName = "GET All Shoplists - OkResult")]
        public async void GetShoplist_Return_OkResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var parameters = new ShoplistParameters();
            parameters.Page = 1;
            parameters.PageSize = 10;

            //Act
            var data = await controller.GetAll(parameters);

            //Assert
            Assert.IsType<ActionResult<IQueryable<ShoplistDTO>>>(data);
        }

        [Fact(
            Skip ="Método GetAll no controller deve lançar exceção para este teste passar.",
            DisplayName = "GET All Shoplists - BadRequestResult")]
        public async void GetShoplist_Return_BadRequestResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var parameters = new ShoplistParameters();
            parameters.Page = 1;
            parameters.PageSize = 10;

            //Act
            var data = await controller.GetAll(parameters);

            //Assert
            Assert.IsType<BadRequestResult>(data.Result);
        }

        [Fact(DisplayName = "GET ShoplistById - OkResult")]
        public async void GetShoplistById_Return_OkResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var shoplistId = 1;

            // Act
            var data = await controller.GetById(shoplistId);

            // Assert
            Assert.IsType<ActionResult<ShoplistDTO>>(data);
        }

        [Fact(DisplayName = "GET ShoplistById - NotFoundResult")]
        public async void GetShoplistById_ReturnNotFoundResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var shoplistId = 9999;

            // Act
            var data = await controller.GetById(shoplistId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact(DisplayName = "POST Shoplist - OkResult")]
        public async void PostShoplist_ReturnOkResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var newShoplist = new ShoplistDTO()
            { Name = "Teste unitário - POST (Shoplist)", Description = "Teste unitário - POST (Shoplist)" };

            // Act
            var data = await controller.Add(newShoplist);

            // Assert
            Assert.IsType<ActionResult<ShoplistDTO>>(data);
        }

        [Fact(
            Skip = "Método Add controller deve lançar exceção para este teste passar.",
            DisplayName = "POST Shoplist - BadRequestResult")]
        public async void PostShoplist_BadRequestResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var newShoplist = new ShoplistDTO()
            { Name = "Teste unitário - POST - BadRequest (Shoplist)", Description = "Teste unitário - POST - BadRequest (Shoplist)" };

            // Act
            var data = await controller.Add(newShoplist);

            // Assert
            Assert.IsType<BadRequestResult>(data.Result);
        }

        [Fact(DisplayName = "PUT Shoplist - OkResult")]
        public async void PutShoplist_ReturnOkResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var shoplistId = 3;

            // Act
            var exinstingShoplist = await controller.GetById(shoplistId);
            var result = exinstingShoplist.Should().BeAssignableTo<ActionResult<ShoplistDTO>>().Subject;

            var shoplistDTO = new ShoplistDTO();
            shoplistDTO.Id = shoplistId;
            shoplistDTO.Name = $"Teste unitário - PUT (Shoplist) - Id modificado: {shoplistId}";
            shoplistDTO.Description = $"Teste unitário - PUT (Shoplist) - Id modificado: {shoplistId}";

            var updatedData = await controller.Update(shoplistId, shoplistDTO);

            // Assert
            Assert.IsType<ActionResult<ShoplistDTO>>(updatedData);
        }

        [Fact(DisplayName = "PUT Shoplist - NotFoundResult")]
        public async void PutShoplist_ReturnNotFoundResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var shoplistId = 9999;

            // Act
            var data = await controller.GetById(shoplistId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact(DisplayName = "DELETE Shoplist - OkResult")]
        public async void DeleteShoplist_ReturnOkResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var shoplistId = 111;

            // Act
            var data = await controller.DeleteShoplist(shoplistId);

            // Assert
            Assert.IsType<OkObjectResult>(data.Result);
        }

        [Fact(DisplayName = "DELETE Shoplist - NotFoundResult")]
        public async void DeleteShoplist_ReturnNotFoundResult()
        {
            // Arrange
            var controller = new ShoplistController(repository, mapper);
            var shoplistId = 9999;

            // Act
            var data = await controller.DeleteShoplist(shoplistId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(data.Result);
        }
    }
}
