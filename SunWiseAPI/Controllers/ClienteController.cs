using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunWiseAPI.Models;
using SunWiseAPI.Repositories;

namespace SunWiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }


        /// <summary>
        /// Método para pegar o cliente pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetCliente(id);

                if (cliente == null) return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método para pegar todos os clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Cliente>> GetClientes()
        {
            try
            {
                return Ok(await _clienteRepository.GetClientes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar um cliente
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Cliente>> AddClientes([FromBody] Cliente cliente)
        {

            try
            {
                if (cliente == null) return BadRequest();

                var createCli = await _clienteRepository.AddCliente(cliente);

                return CreatedAtAction(nameof(GetClienteById),
                    new { id = createCli.Id }, createCli);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método para atualizar um cliente
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Cliente>> UpdateCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null) return BadRequest();

                var cliToUpdate = await _clienteRepository.GetCliente(cliente.Id);
                if (cliToUpdate == null) return BadRequest("Cliente não encontrado");

                var result = await _clienteRepository.UpdateCliente(cliente);

                if (result == null) return BadRequest();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método para deletar um cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public void DeleteCliente(int id)
        {
            _clienteRepository.DeleteCliente(id);
        }
    }
}
