using GestaoObrigacoes.Dados;
using GestaoObrigacoes.DTOs;
using GestaoObrigacoes.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestaoObrigacoes.Servicos
{
    public class ServicoObrigacao
    {
        private readonly AppDbContext _context;

        public ServicoObrigacao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ObrigacaoRespostaDTO>> ObterTodasObrigacoes()
        {
            return await _context.ObrigacoesAcessorias
                .Include(o => o.Empresa)
                .Select(o => new ObrigacaoRespostaDTO
                {
                    Id = o.Id,
                    Nome = o.Nome,
                    Periodicidade = o.Periodicidade,
                    EmpresaId = o.EmpresaId,
                    EmpresaNome = o.Empresa!.Nome
                })
                .ToListAsync();
        }

        public async Task<List<ObrigacaoRespostaDTO>> ObterObrigacoesPorEmpresa(int empresaId)
        {
            return await _context.ObrigacoesAcessorias
                .Where(o => o.EmpresaId == empresaId)
                .Include(o => o.Empresa)
                .Select(o => new ObrigacaoRespostaDTO
                {
                    Id = o.Id,
                    Nome = o.Nome,
                    Periodicidade = o.Periodicidade,
                    EmpresaId = o.EmpresaId,
                    EmpresaNome = o.Empresa!.Nome
                })
                .ToListAsync();
        }

        public async Task<ObrigacaoRespostaDTO?> ObterObrigacaoPorId(int id)
        {
            var obrigacao = await _context.ObrigacoesAcessorias
                .Include(o => o.Empresa)
                .FirstOrDefaultAsync(o => o.Id == id);
                
            if (obrigacao == null)
                return null;

            return new ObrigacaoRespostaDTO
            {
                Id = obrigacao.Id,
                Nome = obrigacao.Nome,
                Periodicidade = obrigacao.Periodicidade,
                EmpresaId = obrigacao.EmpresaId,
                EmpresaNome = obrigacao.Empresa?.Nome
            };
        }

        public async Task<ObrigacaoRespostaDTO?> CriarObrigacao(ObrigacaoCriarDTO obrigacaoDto)
        {
            // Verificar se empresa existe
            var empresa = await _context.Empresas.FindAsync(obrigacaoDto.EmpresaId);
            if (empresa == null)
                return null;

            var obrigacao = new ObrigacaoAcessoria
            {
                Nome = obrigacaoDto.Nome,
                Periodicidade = obrigacaoDto.Periodicidade,
                EmpresaId = obrigacaoDto.EmpresaId
            };

            _context.ObrigacoesAcessorias.Add(obrigacao);
            await _context.SaveChangesAsync();

            return new ObrigacaoRespostaDTO
            {
                Id = obrigacao.Id,
                Nome = obrigacao.Nome,
                Periodicidade = obrigacao.Periodicidade,
                EmpresaId = obrigacao.EmpresaId,
                EmpresaNome = empresa.Nome
            };
        }

        public async Task<ObrigacaoRespostaDTO?> AtualizarObrigacao(int id, ObrigacaoAtualizarDTO obrigacaoDto)
        {
            var obrigacao = await _context.ObrigacoesAcessorias
                .Include(o => o.Empresa)
                .FirstOrDefaultAsync(o => o.Id == id);
                
            if (obrigacao == null)
                return null;

            if (obrigacaoDto.Nome != null)
                obrigacao.Nome = obrigacaoDto.Nome;
            
            if (obrigacaoDto.Periodicidade.HasValue)
                obrigacao.Periodicidade = obrigacaoDto.Periodicidade.Value;
            
            if (obrigacaoDto.EmpresaId.HasValue)
            {
                var empresa = await _context.Empresas.FindAsync(obrigacaoDto.EmpresaId.Value);
                if (empresa == null)
                    return null;
                    
                obrigacao.EmpresaId = obrigacaoDto.EmpresaId.Value;
            }

            await _context.SaveChangesAsync();

            return new ObrigacaoRespostaDTO
            {
                Id = obrigacao.Id,
                Nome = obrigacao.Nome,
                Periodicidade = obrigacao.Periodicidade,
                EmpresaId = obrigacao.EmpresaId,
                EmpresaNome = obrigacao.Empresa?.Nome
            };
        }

        public async Task<bool> ExcluirObrigacao(int id)
        {
            var obrigacao = await _context.ObrigacoesAcessorias.FindAsync(id);
            if (obrigacao == null)
                return false;

            _context.ObrigacoesAcessorias.Remove(obrigacao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}