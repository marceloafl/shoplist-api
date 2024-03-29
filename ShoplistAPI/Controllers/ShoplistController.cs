﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Repository;
using System.Diagnostics;

namespace ShoplistAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ShoplistController: ControllerBase
    {
        private readonly IShoplistRepository _shoplistRepository;
        private readonly IMapper _mapper;

        public ShoplistController(IShoplistRepository shoplistRepository, IMapper mapper)
        {
            _shoplistRepository = shoplistRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna listagem com todas as listas de compras cadastradas
        /// </summary>
        /// <response code="200">Listagem com listas de compras obtida com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult<List<ShoplistDTO>>> GetAll()
        {
            try
            {
                List<ShoplistDTO> shoplists = await _shoplistRepository.GetAll();
                return Ok(shoplists);
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
        public async Task<ActionResult<ShoplistDTO>> GetById(int id)
        {
            var shoplistWithQueriedId = await _shoplistRepository.GetById(id);

            var searchedShoplist = _mapper.Map<ShoplistDTO>(shoplistWithQueriedId);

            return searchedShoplist == null ? NotFound() : Ok(searchedShoplist);
        }

        /// <summary>
        /// Cadastra uma lista de compras.
        /// </summary>
        /// <param name="shoplistDTO">Modelo da lista de compras.</param>
        /// <response code="201">Lista de compras cadastrada com sucesso.</response>
        [HttpPost]
        public async Task<ActionResult<Shoplist>> Add([FromBody] ShoplistDTO shoplistDTO)
        {
            try
            {
                var newShoplist = _mapper.Map<Shoplist>(shoplistDTO);
                await _shoplistRepository.Add(newShoplist);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = newShoplist.Id },
                    newShoplist
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
        /// <param name="shoplist">ID da lista de compras.</param>
        /// <param name="shoplist">Modelo da lista de compras.</param>
        /// <response code="204">Lista de compras alterada com sucesso.</response>
        /// <response code="404">Não foi encontrada lista de compras com ID especificado.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<Shoplist>> Update(int id, [FromBody] ShoplistDTO shoplist)
        {
            var shoplistWithQueriedId = await _shoplistRepository.GetById(id);
            var shoplistToUpdate = _mapper.Map(shoplist, shoplistWithQueriedId);

            _shoplistRepository.Update(id, shoplistToUpdate);

            return shoplistToUpdate == null ? NotFound() : Ok(shoplist);
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
            var shoplistToDelete = await _shoplistRepository.GetById(id);
        
            if (shoplistToDelete == null) return NotFound("Não foi encontrada lista de compras com ID especificado");

            _shoplistRepository.Delete(shoplistToDelete);
            return Ok("Lista de compras deletada com sucesso.");
        }
    }
}
