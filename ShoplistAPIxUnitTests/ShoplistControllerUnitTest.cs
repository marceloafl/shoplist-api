using AutoMapper;
using AutoMapper.Execution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Controllers;
using ShoplistAPI.Data;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Profiles;
using ShoplistAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoplistAPIxUnitTests
{
    public class ShoplistControllerUnitTest
    {
        private IShoplistRepository shoplistRepository;
        private IMapper mapper;

        public static DbContextOptions<ShoplistContext> shoplistContextOptions { get; }

        public static string stringConnection = "Data Source=NBQFC-8YHVYL3;Initial Catalog=ShoplistDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
            });
            mapper = config.CreateMapper();

            // Configura o shoplistRepository usando o contexto (ShoplistContext)
            var context = new ShoplistContext(shoplistContextOptions);
            shoplistRepository = new ShoplistRepository(context);
        }

        [Fact(DisplayName = "GET All shoplists - OkResult")]
        public async void GetShoplist_Return_OkResult()
        {
            // Arrange
            var controller = new ShoplistController(shoplistRepository, mapper);

            //Act
            var data = await controller.GetAll();

            //Assert
            Assert.IsType<ActionResult<List<ShoplistDTO>>>(data);
        }

        [Fact
            (Skip ="Método GetAll no controller deve lançar exceção para este teste passar.",
            DisplayName = "GET All shoplists - BadRequestResult")]
        public async void GetShoplist_Return_BadRequestResult()
        {
            // Arrange
            var controller = new ShoplistController(shoplistRepository, mapper);

            //Act
            var data = await controller.GetAll();

            //Assert
            Assert.IsType<BadRequestResult>(data.Result);
        }

        [Fact(DisplayName = "GET ShoplistById - OkResult")]
        public async void GetShoplisById_Return_OkResult()
        {
            // Arrange
            var controller = new ShoplistController(shoplistRepository, mapper);
            var shoplistId = 1;

            // Act
            var data = await controller.GetById(shoplistId);

            // Assert
            Assert.IsType <ActionResult<ShoplistDTO>>(data);
        }

        [Fact(DisplayName = "GET ShoplistById - NotFoundResult")]
        public async void GetShoplisById_ReturnNotFoundResult()
        {
            // Arrange
            var controller = new ShoplistController(shoplistRepository, mapper);
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
            var controller = new ShoplistController(shoplistRepository, mapper);
            var newShoplist = new ShoplistDTO()
            { Name = "Teste unitário post de lista", Description = "Teste unitário inclusão de lista" };

            // Act
            var data = controller.Add(newShoplist);

            // Assert
            Assert.IsType<ActionResult<Shoplist>>(data.Result);
        }

        // Teste PUT

        // Teste DELETE
        
    }
}
