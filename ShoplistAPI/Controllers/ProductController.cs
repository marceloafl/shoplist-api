using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Repository;

namespace ShoplistAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna listagem com todos os produtos cadastrados
        /// </summary>
        /// <response code="200">Listagem de produtos obtida com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            List<ProductDTO> products = await _productRepository.GetAll();

            return Ok(products);
        }

        /// <summary>
        /// Retorna um produto específio por ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <response code="200">Produto obtido com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com o ID especificado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var productWithQueriedId = await _productRepository.GetById(id);

            var searchedProduct = _mapper.Map<ProductDTO>(productWithQueriedId);

            return Ok(searchedProduct);
        }

        /// <summary>
        /// Cadastra um produto.
        /// </summary>
        /// <param name="productDTO">Modelo de produto.</param>
        /// <response code="201">Produto cadastrado com sucesso.</response>
        [HttpPost]
        public async Task<ActionResult<Product>> Add([FromBody] ProductDTO productDTO)
        {
            var newProduct = _mapper.Map<Product>(productDTO);
            await _productRepository.Add(newProduct);

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
        public async Task<ActionResult<Product>> Update(int id, [FromBody] ProductDTO product)
        {
            var productWithQueriedId = await _productRepository.GetById(id);
            var productToUpdate = _mapper.Map(product, productWithQueriedId);

            _productRepository.Update(id, productToUpdate);
            return Ok(product);
        }

        /// <summary>
        /// Deleta um produto específico.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <response code="204">Produto deletado com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com ID especificado.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {

            var productToDelete = await _productRepository.GetById(id);
            _productRepository.Delete(productToDelete);
            return Ok(productToDelete);
        }
    }
}
