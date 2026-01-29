using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Repositories.Lucros;
using GestaoFinanceira.Models;
using GestaoFinanceira.DTOs.Lucro;
using GestaoFinanceira.Repositories.CateLucro;
using Microsoft.Identity.Client;

namespace GestaoFinanceira.Services.Lucros
{
    public class LucroService : ILucroService
    {
        private readonly ILucroRepository _crud;
        private readonly INotificador _notificador;
        private readonly ICateLucroRepository _categoria;

        public LucroService(ILucroRepository lucroRepository, INotificador notificador, ICateLucroRepository categoria)
        {
            _crud = lucroRepository;
            _notificador = notificador;
            _categoria = categoria;
        }

        //Listar Lucros
        public async Task<List<LucroResponseDto>> ListarLucros(string userId, int? mes, string? nomeCategoria)
        {
            var lucros = await _crud.GetLucros(l => l.UsuarioId == userId);
            if (mes > 0 || mes != null)
            {
                lucros = lucros.Where(l => l.Data.Month == mes).ToList();
            }
            if (!string.IsNullOrWhiteSpace(nomeCategoria))
            {
                lucros = lucros.Where(l => l.CategoriaLucro != null && l.CategoriaLucro.Nome.Equals(nomeCategoria, StringComparison.OrdinalIgnoreCase)).ToList(); // Filtra por nome da categoria, ignorando maiúsculas/minúsculas
            }
            return lucros.Select(l => new LucroResponseDto
            {
                Id = l.LucroId,
                Valor = l.Valor,
                Data = l.Data,
                Descricao = l.Descricao,
                Categoria = l.CategoriaLucro?.Nome ?? "Sem categoria"
            }).ToList();
        }

        //Buscar Lucro por Id
        public async Task<LucroResponseDto?> BuscarLucroPorId(Guid lucroId, string userId)
        {
            var lucro = await _crud.FindLucroById(lucroId);
            var categoria = await _categoria.GetCategoriaLucroById(lucro.CategoriaLucroId);
            if (lucro == null || lucro.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Lucro não encontrado"));
                return null;
            }
            return new LucroResponseDto
            {
                Id = lucro.LucroId,
                Valor = lucro.Valor,
                Data = lucro.Data,
                Descricao = lucro.Descricao,
                Categoria = categoria.Nome
            };
        }

        //Criar Lucro
        public async Task<LucroResponseDto> CriarLucro(LucroCreateDto dto, string userId)
        {
            if (dto.Valor <= 0)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("O valor do lucro deve ser maior que zero"));
                return null;
            }
            var categoria = await _categoria.GetCategoriaLucroById(dto.Categoria);

            if (categoria == null || categoria.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Categoria de lucro inválida"));
                return null;
            }
            var novoLucro = new Lucro
            {
                Valor = dto.Valor,
                Data = dto.Data,
                Descricao = dto.Descricao,
                CategoriaLucroId = dto.Categoria,
                UsuarioId = userId
            };

            await _crud.CreateLucro(novoLucro);
            return new LucroResponseDto
            {
                Id = novoLucro.LucroId,
                Valor = novoLucro.Valor,
                Data = novoLucro.Data,
                Descricao = novoLucro.Descricao,
                Categoria = categoria.Nome
            };
        }

        //Atualizar Lucro
        public async Task<LucroResponseDto> AtualizarLucro(Guid lucroId, LucroCreateDto dto, string userId)
        {
            var lucroExistente = await _crud.FindLucroById(lucroId);
            if (lucroExistente == null || lucroExistente.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Lucro não encontrado"));
                return null;
            }
            var categoria = await _categoria.GetCategoriaLucroById(dto.Categoria);
            if (categoria == null || categoria.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Categoria de lucro inválida"));
                return null;
            }
            lucroExistente.Valor = dto.Valor;
            lucroExistente.Descricao = dto.Descricao;
            lucroExistente.CategoriaLucroId = dto.Categoria;
            await _crud.UpdateLucro(lucroExistente);
            return new LucroResponseDto
            {
                Id = lucroExistente.LucroId,
                Valor = lucroExistente.Valor,
                Data = lucroExistente.Data,
                Descricao = lucroExistente.Descricao,
                Categoria = categoria.Nome
            };
        }

        //Deletar Lucro
        public async Task<string> DeletarLucro(Guid id)
        {
            var lucroExistente = await _crud.FindLucroById(id);
            if (lucroExistente == null)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Lucro não encontrado"));
                return null;
            }
            await _crud.DeleteLucro(lucroExistente);
            return "Lucro deletado com sucesso";
        }
    }
}

