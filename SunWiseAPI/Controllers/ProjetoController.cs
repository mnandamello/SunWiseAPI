using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunWiseAPI.Models;
using SunWiseAPI.Repositories;
using SunWiseAPI.Repositories.Implementation;

namespace SunWiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoRepository _projetoRepository;
        public ProjetoController(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        /// <summary>
        /// Método para pegar o projeto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<Projeto>> GetProjetoById(int id)
        {
            try
            {
                var projeto = await _projetoRepository.GetProjetoById(id);

                if (projeto == null) return NotFound();

                return Ok(projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método para pegar o projeto pelo id do usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Projeto>> GetProjetoByUserId(string id)
        {
            try
            {
                var projeto = await _projetoRepository.GetProjetoByUserId(id);

                if (projeto == null) return NotFound();

                return Ok(projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método para pegar todos os projetos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Projeto>> GetProjetos()
        {
            try
            {
                return Ok(await _projetoRepository.GetProjetos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar um Projeto
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Projeto>> AddProjeto([FromBody] Projeto projeto)
        {

            try
            {
                if (projeto == null) return BadRequest();

                var createProj = await _projetoRepository.AddProjeto(projeto);

                return CreatedAtAction(nameof(GetProjetoById),
                    new { id = createProj.Id }, createProj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Método para deletar um projeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public void DeleteProjeto(int id)
        {
            _projetoRepository.DeleteProjeto(id);
        }
    }
}
