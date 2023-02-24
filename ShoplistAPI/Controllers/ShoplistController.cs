using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data;
using ShoplistAPI.Model;
using System.Diagnostics;

namespace ShoplistAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoplistController: ControllerBase
    {
        private ShoplistContext _context;

        public ShoplistController(ShoplistContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna listagem com todas as listas de compras cadastradas
        /// </summary>
        /// <response code="200">Listagem com listas de compras obtida com sucesso.</response>
        [HttpGet]
        public IEnumerable<Shoplist> GetAll()
        {
            return _context.Shoplists.ToList();
        }

        /// <summary>
        /// Retorna uma lista de compras específica por ID.
        /// </summary>
        /// <param name="id">ID da lista de compras.</param>
        /// <response code="200">Lista de compras obtida com sucesso.</response>
        /// <response code="404">Não foi encontrado lista de compras com o ID especificado.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var shoplistWithQueriedId = _context.Shoplists.FirstOrDefault(shoplist => shoplist.Id == id);
            if (shoplistWithQueriedId == null) return NotFound();

            return Ok(shoplistWithQueriedId);
        }

        /// <summary>
        /// Cadastra uma lista de compras.
        /// </summary>
        /// <param name="shoplist">Modelo da lista de compras.</param>
        /// <response code="201">Lista de compras cadastrada com sucesso.</response>
        [HttpPost]
        public IActionResult Add([FromBody] Shoplist shoplist)
        {
            _context.Shoplists.Add(shoplist);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),
                new { id = shoplist.Id },
                shoplist);

        }

        /// <summary>
        /// Altera uma lista de compras específica por ID.
        /// </summary> 
        /// <param name="shoplist">ID da lista de compras.</param>
        /// <param name="shoplist">Modelo da lista de compras.</param>
        /// <response code="204">Lista de compras alterada com sucesso.</response>
        /// <response code="404">Não foi encontrada lista de compras com ID especificado.</response>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Shoplist shoplist)
        {
            var shoplistWithQueriedId = _context.Shoplists.FirstOrDefault(shoplist => shoplist.Id == id);
            if (shoplistWithQueriedId == null) return NotFound();

            shoplistWithQueriedId.Name = shoplist.Name;
            shoplistWithQueriedId.Description = shoplist.Description;
            shoplistWithQueriedId.Products = shoplist.Products;

            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta uma lista de compras específica.
        /// </summary>
        /// <param name="id">ID da lista de compras.</param>
        /// <response code="204">Lista de compras deletada com sucesso.</response>
        /// <response code="404">Não foi encontrada lista de compras com ID especificado.</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteShoplist(int id)
        {
            var shoplistWithQueriedId = _context.Shoplists.FirstOrDefault(shoplist => shoplist.Id == id);
            if (shoplistWithQueriedId == null) return NotFound();

            _context.Remove(shoplistWithQueriedId);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
