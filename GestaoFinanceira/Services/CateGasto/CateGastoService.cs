using GestaoFinanceira.DTOs.CategoriaGasto;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Repositories.CateGasto;
using GestaoFinanceira.Models;

namespace GestaoFinanceira.Services.CateGasto
{
    public class CateGastoService : ICateGastoService
    {
        private readonly INotificador _notificador;
        private readonly ICateGastoRepository _crud;

        public CateGastoService(INotificador notificador, ICateGastoRepository crud)
        {
            _notificador = notificador;
            _crud = crud;
        }

        //Listar Categorias de Gasto
        public async Task<List<CategoriaGastoResponseDto>> ListarCategoriasGasto(string userId)
        {
            var categorias = await _crud.GetCategoriaGasto(c => c.UsuarioId == userId);
            return categorias.Select(c => new CategoriaGastoResponseDto
            {
                Id = c.CategoriaGastoId,
                Nome = c.Nome,
                Descricao = c.Descricao,
            }).ToList();
        }

        //Buscar Categoria de Gasto por Id
        public async Task<CategoriaGastoResponseDto> BuscarCategoriaGastoPorId(Guid categoriaGastoId, string userId)
        {
            var categoria = await _crud.FindCategoriaGastoById(categoriaGastoId);
            if (categoria == null || categoria.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Categoria de Gasto não encontrada"));
                return null;
            }
            return new CategoriaGastoResponseDto
            {
                Id = categoria.CategoriaGastoId,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
            };
        }

        //Criar Categoria de Gasto
        public async Task<CategoriaGastoResponseDto> CriarCategoriaGasto(CategoriaGastoCreateDto dto, string userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
            {
                _notificador.Adicionar(new Notificacao.Notificacao("O nome da categoria de gasto é obrigatório"));
                return null;
            }
            var novaCategoria = new CategoriaGasto
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                UsuarioId = userId
            };
            var categoriaCriada = await _crud.CreateCategoriaGasto(novaCategoria);
            return new CategoriaGastoResponseDto
            {
                Id = categoriaCriada.CategoriaGastoId,
                Nome = categoriaCriada.Nome,
                Descricao = categoriaCriada.Descricao,
            };
        }

        //Atualizar Categoria de Gasto
        public async Task<CategoriaGastoResponseDto> AtualizarCategoriaGasto(Guid categoriaGastoId, CategoriaGastoCreateDto dto, string userId)
        {
            var categoriaExistente = await _crud.FindCategoriaGastoById(categoriaGastoId);
            if (categoriaExistente == null || categoriaExistente.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Categoria de Gasto não encontrada"));
                return null;
            }
            if (string.IsNullOrWhiteSpace(dto.Nome))
            {
                _notificador.Adicionar(new Notificacao.Notificacao("O nome da categoria de gasto é obrigatório"));
                return null;
            }
            categoriaExistente.Nome = dto.Nome;
            categoriaExistente.Descricao = dto.Descricao;
            var categoriaAtualizada = await _crud.UpdateCategoriaGasto(categoriaExistente);
            return new CategoriaGastoResponseDto
            {
                Id = categoriaAtualizada.CategoriaGastoId,
                Nome = categoriaAtualizada.Nome,
                Descricao = categoriaAtualizada.Descricao,
            };
        }

        //Deletar Categoria de Gasto
        public async Task<string> DeletarCategoriaGasto(Guid categoriaGastoId, string userId)
        {
            var categoriaExistente = await _crud.FindCategoriaGastoById(categoriaGastoId);
            if (categoriaExistente == null || categoriaExistente.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Categoria de Gasto não encontrada"));
                return null;
            }
            await _crud.DeleteCategoriaGasto(categoriaExistente);

            var resposta = "Categoria de Gasto deletada com sucesso";

            return resposta;
        }
    }
}
