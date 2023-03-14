using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Model.ViewModel;
using ShoplistAPI.Pagination;
using ShoplistAPI.Repository;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace ShoplistAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ShoplistController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoplistController(IUnitOfWork context, IMapper mapper)
        {
            _unitOfWork = context;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna listas de compras",
            Description = "Retorna listagem com todas as listas de compras cadastradas"
        )]
        [SwaggerResponse( 200, "Listagem com listas de compras obtida com sucesso.", typeof( ShoplistResponse ) )]
        [SwaggerResponse( 400, "Ocorreu um erro ao tentar processar a solicitação.")]
        public async Task<ActionResult<IQueryable<ShoplistResponse>>> GetAll([FromQuery] ShoplistParameters shoplistParameters)
        {
            try
            {
                var shoplists = await _unitOfWork.ShoplistRepository
                    .GetShoplists(shoplistParameters);

                var metadata = new
                {
                    shoplists.TotalCount,
                    shoplists.PageSize,
                    shoplists.CurrentPage,
                    shoplists.TotalPages,
                    shoplists.HasNext,
                    shoplists.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

                var shoplistDto = _mapper.Map<List<ShoplistDTO>>(shoplists);
                ShoplistResponse shoplistResponse = new ShoplistResponse(shoplistDto);

                return Ok(shoplistResponse);
        }
            catch(Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Retorna uma lista de compras específica por ID"
        )]
        [SwaggerResponse(200, "Lista de compras obtida com sucesso.", typeof(ShoplistResponse))]
        [SwaggerResponse(404, "Não foi encontrado lista de compras com o ID especificado.")]
        public async Task<ActionResult<ProductResponse>> GetById(int id)
        {
            var shoplistWithQueriedId = await _unitOfWork.ShoplistRepository.GetById(sl => sl.Id == id);

            if (shoplistWithQueriedId == null) return NotFound();

            var searchedShoplist = _mapper.Map<ShoplistDTO>(shoplistWithQueriedId);

            ShoplistResponse shoplistResponse = new ShoplistResponse();
            shoplistResponse.Shoplists.Add(searchedShoplist);

            return Ok(shoplistResponse);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra uma lista de compras."
        )]
        [SwaggerResponse(201, "Lista de compras cadastrada com sucesso.")]
        [SwaggerResponse(400, "")]
        public async Task<ActionResult> Add([FromBody] ShoplistDTO shoplistDto)
        {
            try
            {
                var newShoplist = _mapper.Map<Shoplist>(shoplistDto);
                _unitOfWork.ShoplistRepository.Add(newShoplist);
                await _unitOfWork.Commit();

                var newShoplistDto = _mapper.Map<ShoplistDTO>(newShoplist);

                //ShoplistResponse shoplistResponse = new ShoplistResponse();
                //shoplistResponse.Shoplists.Add(newShoplistDto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = newShoplist.Id },
                    newShoplistDto
                    );
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza uma lista de compras específica por ID."
        )]
        [SwaggerResponse(204, "Lista de compras atualizada com sucesso.")]
        [SwaggerResponse(400, "")]
        public async Task<ActionResult> Update(int id, [FromBody] ShoplistDTO shoplistDto)
        {
            if (id != shoplistDto.Id) return BadRequest();

            var shoplistToUpdate = await _unitOfWork.ShoplistRepository.GetById(sl => sl.Id == id);
            if (shoplistToUpdate == null) return NotFound("Não foi encontrada lista de compras com ID especificado.");

            var shoplistChanged = _mapper.Map<Shoplist>(shoplistDto);

            _unitOfWork.ShoplistRepository.Update(shoplistChanged);
            await _unitOfWork.Commit();

            var shoplistChangedDto = _mapper.Map<ShoplistDTO>(shoplistChanged);
            return Ok("Lista de compras atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deleta uma lista de compras específica."
        )]
        [SwaggerResponse(204, "Lista de compras deletada com sucesso.")]
        [SwaggerResponse(404, "Não foi encontrada lista de compras com ID especificado.")]
        public async Task<ActionResult> DeleteShoplist(int id)
        {
            var shoplistToDelete = await _unitOfWork.ShoplistRepository.GetById(sl => sl.Id == id);
            if (shoplistToDelete == null) return NotFound("Não foi encontrada lista de compras com ID especificado");

            _unitOfWork.ShoplistRepository.Delete(shoplistToDelete);
            await _unitOfWork.Commit();

            return Ok("Lista de compras deletada com sucesso.");
        }
    }
}
