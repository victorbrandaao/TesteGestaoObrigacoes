using GestaoObrigacoes.DTOs;
using GestaoObrigacoes.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GestaoObrigacoes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly ServicoEmpresa _servicoEmpresa;

        public EmpresaController(ServicoEmpresa servicoEmpresa)
        {
            _servicoEmpresa = servicoEmpresa;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpresaRespostaDTO>>> ObterTodasEmpresas()
        {
            var empresas = await _servicoEmpresa.ObterTodasEmpresas();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaRespostaDTO>> ObterEmpresaPorId(int id)
        {
            var empresa = await _servicoEmpresa.ObterEmpresaPorId(id);
            if (empresa == null)
                return NotFound($"Empresa com ID {id} não encontrada");

            return Ok(empresa);
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaRespostaDTO>> CriarEmpresa(EmpresaCriarDTO empresaDto)
        {
            try
            {
                var novaEmpresa = await _servicoEmpresa.CriarEmpresa(empresaDto);
                return CreatedAtAction(nameof(ObterEmpresaPorId), new { id = novaEmpresa.Id }, novaEmpresa);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar empresa: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmpresaRespostaDTO>> AtualizarEmpresa(int id, EmpresaAtualizarDTO empresaDto)
        {
            try
            {
                var empresaAtualizada = await _servicoEmpresa.AtualizarEmpresa(id, empresaDto);
                if (empresaAtualizada == null)
                    return NotFound($"Empresa com ID {id} não encontrada");

                return Ok(empresaAtualizada);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar empresa: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirEmpresa(int id)
        {
            var resultado = await _servicoEmpresa.ExcluirEmpresa(id);
            if (!resultado)
                return NotFound($"Empresa com ID {id} não encontrada");

            return NoContent();
        }
    }
}