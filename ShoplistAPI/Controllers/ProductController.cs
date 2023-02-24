using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data;
using ShoplistAPI.Model;

namespace ShoplistAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private ShoplistContext _context;

        public ProductController(ShoplistContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados
        /// </summary>
        /// <response code="200">Lista de produtos obtida com sucesso.</response>
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        /// <summary>
        /// Retorna um produto específico por ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <response code="200">Produto obtido com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com o ID especificado.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var productWithQueriedId = _context.Products.FirstOrDefault(product => product.Id == id);
            if (productWithQueriedId == null) return NotFound();

            return Ok(productWithQueriedId);
        }

        /// <summary>
        /// Cadastra um produto.
        /// </summary>
        /// <param name="product">Modelo do produto.</param>
        /// <response code="201">Produto cadastrado com sucesso.</response>
        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),
                new { id = product.Id },
                product);

        }

        /// <summary>
        /// Altera um produto específico por ID.
        /// </summary> 
        /// <param name="id">ID do produto.</param>
        /// <param name="product">Modelo do produto.</param>
        /// <response code="204">Produto alterado com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com ID especificado.</response>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            var productWithQueriedId = _context.Products.FirstOrDefault(product => product.Id == id);
            if (productWithQueriedId == null) return NotFound();

            productWithQueriedId.Name = product.Name;
            productWithQueriedId.Brand = product.Brand;
            productWithQueriedId.Description = product.Description;
            productWithQueriedId.Number = product.Number;
            productWithQueriedId.ShoplistId = product.ShoplistId;

            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta um produto específico.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <response code="204">Produto deletado com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com ID especificado.</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productWithQueriedId = _context.Products.FirstOrDefault(product => product.Id == id);
            if (productWithQueriedId == null) return NotFound();

            _context.Remove(productWithQueriedId);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
