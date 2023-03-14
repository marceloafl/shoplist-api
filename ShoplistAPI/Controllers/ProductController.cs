using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Pagination;
using ShoplistAPI.Repository;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna produtos cadastrados",
            Description = "Retorna listagem com todos os produtos cadastrados"
        )]
        [SwaggerResponse(200, "Listagem de produtos obtida com sucesso.", typeof(ShoplistDTO))]
        [SwaggerResponse(400, "Ocorreu um erro ao tentar processar a solicitação.")]
        public async Task<ActionResult<IQueryable<ProductDTO>>> GetAll([FromQuery] ProductParameters productParameters)
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetProducts(productParameters);

                var metadata = new
                {
                    products.TotalCount,
                    products.PageSize,
                    products.CurrentPage,
                    products.TotalPages,
                    products.HasNext,
                    products.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

                var productDto = _mapper.Map<List<ProductDTO>>(products);
                return Ok(productDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Retorna um produto específio por ID"
        )]
        [SwaggerResponse(200, "Produto obtido com sucesso.", typeof(ShoplistDTO))]
        [SwaggerResponse(404, "Não foi encontrado produto com o ID especificado.")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var productWithQueriedId = await _unitOfWork.ProductRepository.GetById(p => p.Id == id);
            
            if (productWithQueriedId == null) return NotFound();

            var searchedProduct = _mapper.Map<ProductDTO>(productWithQueriedId);
            return Ok(searchedProduct);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra um produto."
        )]
        [SwaggerResponse(201, "Produto cadastrado com sucesso.", typeof(ShoplistDTO))]
        [SwaggerResponse(400, "")]
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

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Altera um produto específico por ID."
        )]
        [SwaggerResponse(204, "Produto alterado com sucesso.", typeof(ShoplistDTO))]
        [SwaggerResponse(404, "Não foi encontrado produto com ID especificado.")]
        public async Task<ActionResult<ProductDTO>> Update(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id) return BadRequest();

            var productChanged = _mapper.Map<Product>(productDto);

            _unitOfWork.ProductRepository.Update(productChanged);
            await _unitOfWork.Commit();

            var productChangedDto = _mapper.Map<ProductDTO>(productChanged);
            return Ok(productChangedDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deleta um produto específico."
        )]
        [SwaggerResponse(204, "Produto deletado com sucesso.", typeof(ShoplistDTO))]
        [SwaggerResponse(404, "Não foi encontrado produto com ID especificado.")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
        {
            var productToDelete = await _unitOfWork.ProductRepository.GetById(p => p.Id == id);
            if (productToDelete == null) return NotFound("Não foi encontrado produto com ID especificado.");

            _unitOfWork.ProductRepository.Delete(productToDelete);
            await _unitOfWork.Commit();

            return Ok("Produto deletado com sucesso.");
        }
    }
}
