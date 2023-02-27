using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data;
using ShoplistAPI.Model;
using ShoplistAPI.Repository;

namespace ShoplistAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados
        /// </summary>
        /// <response code="200">Lista de produtos obtida com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            List<Product> products = await _productRepository.GetAll();
            return Ok(products);
        }

        /// <summary>
        /// Retorna um produto específico por ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <response code="200">Produto obtido com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com o ID especificado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var productWithQueriedId = await _productRepository.GetById(id);
            return Ok(productWithQueriedId);
        }

        /// <summary>
        /// Cadastra um produto.
        /// </summary>
        /// <param name="product">Modelo do produto.</param>
        /// <response code="201">Produto cadastrado com sucesso.</response>
        [HttpPost]
        public async Task<ActionResult<Product>> Add([FromBody] Product product)
        {
            var newProduct = await _productRepository.Add(product);
            return Ok(newProduct);
        }

        /// <summary>
        /// Altera um produto específico por ID.
        /// </summary> 
        /// <param name="id">ID do produto.</param>
        /// <param name="product">Modelo do produto.</param>
        /// <response code="204">Produto alterado com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com ID especificado.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, [FromBody] Product product)
        {
            product.Id = id;
            var newProduct = await _productRepository.Update(id, product);
            return Ok(newProduct);
        }

        /// <summary>
        /// Deleta um produto específico.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <response code="204">Produto deletado com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com ID especificado.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteShoplist(int id)
        {
            bool isProductDeleted = await _productRepository.Delete(id);
            return Ok(isProductDeleted);
        }
    }
}
