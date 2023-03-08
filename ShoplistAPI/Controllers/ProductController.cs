using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Repository;

namespace ShoplistAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork context, IMapper mapper)
        {
            _unitOfWork = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna listagem com todos os produtos cadastrados
        /// </summary>
        /// <response code="200">Listagem de produtos obtida com sucesso.</response>
        /// /// <response code="400">Ocorreu um erro ao tentar processar a solicitação.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<IQueryable<ProductDTO>>> GetAll()
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAll().AsNoTracking().ToListAsync();
                var productDto = _mapper.Map<List<ProductDTO>>(products);
                return Ok(productDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Retorna um produto específio por ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <response code="200">Produto obtido com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com o ID especificado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var productWithQueriedId = await _unitOfWork.ProductRepository.GetById(p => p.Id == id);
            
            if (productWithQueriedId == null) return NotFound();

            var searchedProduct = _mapper.Map<ProductDTO>(productWithQueriedId);
            return Ok(searchedProduct);
        }

        /// <summary>
        /// Cadastra um produto.
        /// </summary>
        /// <param name="productDto">Modelo de produto.</param>
        /// <response code="201">Produto cadastrado com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDTO>> Add([FromBody] ProductDTO productDto)
        {
            try
            {
                var newProduct = _mapper.Map<Product>(productDto);
                _unitOfWork.ProductRepository.Add(newProduct);
                await _unitOfWork.Commit();

                var newProductDto = _mapper.Map<ProductDTO>(newProduct);

                return CreatedAtAction(
                nameof(GetById),
                new { id = newProduct.Id },
                newProductDto
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Altera um produto específico por ID.
        /// </summary> 
        /// <param name="id">ID do produto.</param>
        /// <param name="productDto">Modelo do produto.</param>
        /// <response code="204">Produto alterado com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com ID especificado.</response>
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<Product>> Update(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id) return BadRequest();

            var productChanged = _mapper.Map<Product>(productDto);

            _unitOfWork.ProductRepository.Update(productChanged);
            await _unitOfWork.Commit();

            var productChangedDto = _mapper.Map<ProductDTO>(productChanged);
            return Ok(productChangedDto);
        }

        /// <summary>
        /// Deleta um produto específico.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <response code="204">Produto deletado com sucesso.</response>
        /// <response code="404">Não foi encontrado produto com ID especificado.</response>
        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var productToDelete = await _unitOfWork.ProductRepository.GetById(p => p.Id == id);

            if (productToDelete == null) return NotFound("Não foi encontrado produto com ID especificado.");


            _unitOfWork.ProductRepository.Delete(productToDelete);
            await _unitOfWork.Commit();

            return Ok("Produto deletado com sucesso.");
        }
    }
}
