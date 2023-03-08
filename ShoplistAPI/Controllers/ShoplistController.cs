using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Repository;

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

        /// <summary>
        /// Retorna listagem com todas as listas de compras cadastradas
        /// </summary>
        /// <response code="200">Listagem com listas de compras obtida com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar processar a solicitação.</response>
        [HttpGet]
        public ActionResult<IQueryable<ShoplistDTO>> GetAll()
        {
            try
            {
                var shoplists = _unitOfWork.ShoplistRepository
                    .GetAll()
                    .Select(sl => new Shoplist
                    {
                        Id = sl.Id,
                        Name = sl.Name,
                        Description = sl.Description,
                        Products = sl.Products,
                    })
                    .AsNoTracking()
                    .ToList();

                var shoplistDto = _mapper.Map<List<ShoplistDTO>>(shoplists);
                return Ok(shoplistDto);
        }
            catch(Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Retorna uma lista de compras específica por ID.
        /// </summary>
        /// <param name="id">ID da lista de compras.</param>
        /// <response code="200">Lista de compras obtida com sucesso.</response>
        /// <response code="404">Não foi encontrado lista de compras com o ID especificado.</response>
        [HttpGet("{id}")]
        public ActionResult<ShoplistDTO> GetById(int id)
        {
            var shoplistWithQueriedId = _unitOfWork.ShoplistRepository.GetById(sl => sl.Id == id);

            if (shoplistWithQueriedId == null) return NotFound();

            var searchedShoplist = _mapper.Map<ShoplistDTO>(shoplistWithQueriedId);
            return Ok(searchedShoplist);
        }

        /// <summary>
        /// Cadastra uma lista de compras.
        /// </summary>
        /// <param name="shoplistDto">Modelo da lista de compras.</param>
        /// <response code="201">Lista de compras cadastrada com sucesso.</response>
        [HttpPost]
        public ActionResult<ShoplistDTO> Add([FromBody] ShoplistDTO shoplistDto)
        {
            try
            {
                var newShoplist = _mapper.Map<Shoplist>(shoplistDto);
                _unitOfWork.ShoplistRepository.Add(newShoplist);
                _unitOfWork.Commit();

                var newShoplistDto = _mapper.Map<ShoplistDTO>(newShoplist);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = newShoplist.Id },
                    newShoplistDto
                    );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Altera uma lista de compras específica por ID.
        /// </summary> 
        /// <param name="id">ID da lista de compras.</param>
        /// <param name="shoplistDto">Modelo da lista de compras.</param>
        /// <response code="204">Lista de compras alterada com sucesso.</response>
        /// <response code="400">O id especificado e o id da lista de compras não são iguais.</response>
        [HttpPut("{id}")]
        public ActionResult<ShoplistDTO> Update(int id, [FromBody] ShoplistDTO shoplistDto)
        {
            if (id != shoplistDto.Id) return BadRequest();

            var shoplistChanged = _mapper.Map<Shoplist>(shoplistDto);

            _unitOfWork.ShoplistRepository.Update(shoplistChanged);
            _unitOfWork.Commit();

            var shoplistChangedDto = _mapper.Map<ShoplistDTO>(shoplistChanged);
            return Ok(shoplistChangedDto);
        }

        /// <summary>
        /// Deleta uma lista de compras específica.
        /// </summary>
        /// <param name="id">ID da lista de compras.</param>
        /// <response code="204">Lista de compras deletada com sucesso.</response>
        /// <response code="404">Não foi encontrada lista de compras com ID especificado.</response>
        [HttpDelete("{id}")]
        public ActionResult<ShoplistDTO> DeleteShoplist(int id)
        {
            var shoplistToDelete = _unitOfWork.ShoplistRepository.GetById(sl => sl.Id == id);
        
            if (shoplistToDelete == null) return NotFound("Não foi encontrada lista de compras com ID especificado");

            _unitOfWork.ShoplistRepository.Delete(shoplistToDelete);
            _unitOfWork.Commit();

            return Ok("Lista de compras deletada com sucesso.");
        }
    }
}
