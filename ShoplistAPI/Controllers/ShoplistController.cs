using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data;
using ShoplistAPI.Model;
using ShoplistAPI.Repository;
using System.Diagnostics;

namespace ShoplistAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoplistController: ControllerBase
    {
        private readonly IShoplistRepository _shoplistRepository;

        public ShoplistController(IShoplistRepository shoplistRepository)
        {
            _shoplistRepository = shoplistRepository;
        }

        /// <summary>
        /// Retorna listagem com todas as listas de compras cadastradas
        /// </summary>
        /// <response code="200">Listagem com listas de compras obtida com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult<List<Shoplist>>> GetAll()
        {
            List<Shoplist> shoplists = await _shoplistRepository.GetAll();
            return Ok(shoplists);
        }

        /// <summary>
        /// Retorna uma lista de compras específica por ID.
        /// </summary>
        /// <param name="id">ID da lista de compras.</param>
        /// <response code="200">Lista de compras obtida com sucesso.</response>
        /// <response code="404">Não foi encontrado lista de compras com o ID especificado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Shoplist>> GetById(int id)
        {
            var shoplistWithQueriedId = await _shoplistRepository.GetById(id);
            return Ok(shoplistWithQueriedId);
        }

        /// <summary>
        /// Cadastra uma lista de compras.
        /// </summary>
        /// <param name="shoplist">Modelo da lista de compras.</param>
        /// <response code="201">Lista de compras cadastrada com sucesso.</response>
        [HttpPost]
        public async Task<ActionResult<Shoplist>> Add([FromBody] Shoplist shoplist)
        {
            var newShoplist = await _shoplistRepository.Add(shoplist);
            return Ok(newShoplist);
        }

        /// <summary>
        /// Altera uma lista de compras específica por ID.
        /// </summary> 
        /// <param name="shoplist">ID da lista de compras.</param>
        /// <param name="shoplist">Modelo da lista de compras.</param>
        /// <response code="204">Lista de compras alterada com sucesso.</response>
        /// <response code="404">Não foi encontrada lista de compras com ID especificado.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<Shoplist>> Update(int id, [FromBody] Shoplist shoplist)
        {
            shoplist.Id = id;
            var newShoplist = await _shoplistRepository.Update(id, shoplist);
            return Ok(newShoplist);
        }

        /// <summary>
        /// Deleta uma lista de compras específica.
        /// </summary>
        /// <param name="id">ID da lista de compras.</param>
        /// <response code="204">Lista de compras deletada com sucesso.</response>
        /// <response code="404">Não foi encontrada lista de compras com ID especificado.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shoplist>> DeleteShoplist(int id)
        {
            bool isShopListDeleted = await _shoplistRepository.Delete(id);
            return Ok(isShopListDeleted);
        }
    }
}
