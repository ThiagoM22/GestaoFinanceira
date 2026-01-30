using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Repositories.Gastos;
using GestaoFinanceira.DTOs.Gasto;
using GestaoFinanceira.Repositories.CateGasto;
using GestaoFinanceira.Models;

namespace GestaoFinanceira.Services.Gastos
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _crud;
        private readonly INotificador _notificador;
        private readonly ICateGastoRepository _categoria;

        public GastoService(IGastoRepository gastoRepository, INotificador notificador, ICateGastoRepository categoria)
        {
            _crud = gastoRepository;
            _notificador = notificador;
            _categoria = categoria;
        }

        //Listar Gastos 
        public async Task<List<GastoResponseDto>> ListarGastos(string userId, int? mes, string? nomeCategoria)
        {
            var gastos = await _crud.GetGastoa(g => g.UsuarioId == userId);
            if (mes > 0 || mes != null)
            {
                gastos = gastos.Where(g => g.Data.Month == mes).ToList();
            }
            if (!string.IsNullOrWhiteSpace(nomeCategoria))
            {
                gastos = gastos.Where(g => g.CategoriaGasto != null && g.CategoriaGasto.Nome.Equals(nomeCategoria, StringComparison.OrdinalIgnoreCase)).ToList(); // Filtra por nome da categoria, ignorando maiúsculas/minúsculas
            }
            return gastos.Select(g => new GastoResponseDto
            {
                Id = g.GastoId,
                Valor = g.Valor,
                Data = g.Data,
                Descricao = g.Descricao,
                Categoria = g.CategoriaGasto?.Nome ?? "Sem categoria"
            }).ToList();
        }

        //Buscar Gasto por Id
        public async Task<GastoResponseDto?> BuscarGastoPorId(Guid gastoId, string userId)
        {
            var gasto = await _crud.FindGastoById(gastoId);
            if (gasto == null || gasto.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Gasto não encontrado"));
                return null;
            }
            return new GastoResponseDto
            {
                Id = gasto.GastoId,
                Valor = gasto.Valor,
                Data = gasto.Data,
                Descricao = gasto.Descricao,
                Categoria = gasto.CategoriaGasto?.Nome ?? "Sem categoria"
            };
        }

        //Criar novo Gasto
        public async Task<GastoResponseDto> CriarGasto(GastoCreateDto dto, string userId)
        {
            if (dto.Valor <= 0)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("O valor do gasto deve ser maior que zero"));
                return null;
            }
            var categoria = await _categoria.FindCategoriaGastoById(dto.Categoria);
            if(categoria == null || categoria.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Categoria de gasto não encontrada"));
                return null;
            }
            var gasto = new Gasto
            {
                Valor = dto.Valor,
                Data = dto.Data,
                Descricao = dto.Descricao,
                CategoriaGastoId = dto.Categoria,
                UsuarioId = userId
            };

            await _crud.CreateGasto(gasto);
            return new GastoResponseDto
            {
                Id = gasto.GastoId,
                Valor = gasto.Valor,
                Data = gasto.Data,
                Descricao = gasto.Descricao,
                Categoria = categoria.Nome
            };
        }

        //Atualizar Gasto
        public async Task<GastoResponseDto?> AtualizarGasto(Guid gastoId, GastoCreateDto dto, string userId)
        {
            var gasto = await _crud.FindGastoById(gastoId);
            if (gasto == null || gasto.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Gasto não encontrado"));
                return null;
            }
            if (dto.Valor <= 0)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("O valor do gasto deve ser maior que zero"));
                return null;
            }
            gasto.Valor = dto.Valor;
            gasto.Data = dto.Data;
            gasto.Descricao = dto.Descricao;
            gasto.CategoriaGastoId = dto.Categoria;
            await _crud.UpdateGasto(gasto);
            return new GastoResponseDto
            {
                Id = gasto.GastoId,
                Valor = gasto.Valor,
                Data = gasto.Data,
                Descricao = gasto.Descricao,
                Categoria = gasto.CategoriaGasto?.Nome ?? "Sem categoria"
            };
        }

        //Deletar Gasto
        public async Task<string> DeletarGasto(Guid gastoId)
        {
            var gasto = await _crud.FindGastoById(gastoId);
            await _crud.DeleteGasto(gasto);

            return "Gasto deletado com sucesso";
        }
    }
}
