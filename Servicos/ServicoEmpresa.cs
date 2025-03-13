using GestaoObrigacoes.Dados;
using GestaoObrigacoes.DTOs;
using GestaoObrigacoes.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestaoObrigacoes.Servicos
{
    public class ServicoEmpresa
    {
        private readonly AppDbContext _context;

        public ServicoEmpresa(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpresaRespostaDTO>> ObterTodasEmpresas()
        {
            return await _context.Empresas
                .Select(e => new EmpresaRespostaDTO
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    CNPJ = e.CNPJ,
                    Endereco = e.Endereco,
                    Email = e.Email,
                    Telefone = e.Telefone
                })
                .ToListAsync();
        }

        public async Task<EmpresaRespostaDTO?> ObterEmpresaPorId(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
                return null;

            return new EmpresaRespostaDTO
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                CNPJ = empresa.CNPJ,
                Endereco = empresa.Endereco,
                Email = empresa.Email,
                Telefone = empresa.Telefone
            };
        }

        public async Task<EmpresaRespostaDTO> CriarEmpresa(EmpresaCriarDTO empresaDto)
        {
            var empresa = new Empresa
            {
                Nome = empresaDto.Nome,
                CNPJ = empresaDto.CNPJ,
                Endereco = empresaDto.Endereco,
                Email = empresaDto.Email,
                Telefone = empresaDto.Telefone
            };

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return new EmpresaRespostaDTO
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                CNPJ = empresa.CNPJ,
                Endereco = empresa.Endereco,
                Email = empresa.Email,
                Telefone = empresa.Telefone
            };
        }

        public async Task<EmpresaRespostaDTO?> AtualizarEmpresa(int id, EmpresaAtualizarDTO empresaDto)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
                return null;

            if (empresaDto.Nome != null)
                empresa.Nome = empresaDto.Nome;
            
            if (empresaDto.CNPJ != null)
                empresa.CNPJ = empresaDto.CNPJ;
            
            if (empresaDto.Endereco != null)
                empresa.Endereco = empresaDto.Endereco;
            
            if (empresaDto.Email != null)
                empresa.Email = empresaDto.Email;
            
            if (empresaDto.Telefone != null)
                empresa.Telefone = empresaDto.Telefone;

            await _context.SaveChangesAsync();

            return new EmpresaRespostaDTO
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                CNPJ = empresa.CNPJ,
                Endereco = empresa.Endereco,
                Email = empresa.Email,
                Telefone = empresa.Telefone
            };
        }

        public async Task<bool> ExcluirEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
                return false;

            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}