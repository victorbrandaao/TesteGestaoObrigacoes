using GestaoObrigacoes.DTOs;
using GestaoObrigacoes.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GestaoObrigacoes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObrigacaoController : ControllerBase
    {
        private readonly ServicoObrigacao _servicoObrigacao;

        public ObrigacaoController(ServicoObrigacao servicoObrigacao)
        {
            _servicoObrigacao = servicoObrigacao;
        }

        [HttpGet]
        public async Task<ActionResult<List<ObrigacaoRespostaDTO>>> ObterTodasObrigacoes()
        {
            var obrigacoes = await _servicoObrigacao.ObterTodasObrigacoes();
            return Ok(obrigacoes);
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<ActionResult<List<ObrigacaoRespostaDTO>>> ObterObrigacoesPorEmpresa(int empresaId)
        {
            var obrigacoes = await _servicoObrigacao.ObterObrigacoesPorEmpresa(empresaId);
            return Ok(obrigacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ObrigacaoRespostaDTO>> ObterObrigacaoPorId(int id)
        {
            var obrigacao = await _servicoObrigacao.ObterObrigacaoPorId(id);
            if (obrigacao == null)
                return NotFound($"Obrigação com ID {id} não encontrada");

            return Ok(obrigacao);
        }

        [HttpPost]
        public async Task<ActionResult<ObrigacaoRespostaDTO>> CriarObrigacao(ObrigacaoCriarDTO obrigacaoDto)
        {
            try
            {
                var novaObrigacao = await _servicoObrigacao.CriarObrigacao(obrigacaoDto);
                if (novaObrigacao == null)
                    return BadRequest("Empresa informada não existe");

                return CreatedAtAction(nameof(ObterObrigacaoPorId), new { id = novaObrigacao.Id }, novaObrigacao);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar obrigação: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ObrigacaoRespostaDTO>> AtualizarObrigacao(int id, ObrigacaoAtualizarDTO obrigacaoDto)
        {
            try
            {
                var obrigacaoAtualizada = await _servicoObrigacao.AtualizarObrigacao(id, obrigacaoDto);
                if (obrigacaoAtualizada == null)
                    return NotFound($"Obrigação com ID {id} não encontrada ou empresa informada não existe");

                return Ok(obrigacaoAtualizada);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar obrigação: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirObrigacao(int id)
        {
            var resultado = await _servicoObrigacao.ExcluirObrigacao(id);
            if (!resultado)
                return NotFound($"Obrigação com ID {id} não encontrada");

            return NoContent();
        }
    }
}