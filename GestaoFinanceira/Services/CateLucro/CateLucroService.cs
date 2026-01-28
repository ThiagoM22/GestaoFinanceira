using GestaoFinanceira.DTOs.CategoriaLucro;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Repositories.CateLucro;
using GestaoFinanceira.Models;

namespace GestaoFinanceira.Services.CateLucro
{
    public class CateLucroService : ICateLucroService
    {
        private readonly INotificador _notificador;
        private readonly ICateLucroRepository _crud;

        public CateLucroService(INotificador notificador, ICateLucroRepository crud)
        {
            _notificador = notificador;
            _crud = crud;
        }

        //Listar Categorias de Lucro
        public async Task<List<CategoriaLucroResponseDto>> ListarCategoriasLucro(string userId)
        {
            var categorias = await _crud.GetCategoriaLucros(c => c.UsuarioId == userId );
            return categorias.Select(c => new CategoriaLucroResponseDto
            {
                Nome = c.Nome,
                Descricao = c.Descricao,
            }).ToList();
        }

        //Listar por Id
        public async Task<CategoriaLucroResponseDto> BuscarCategoriaLucroPorId(Guid categoriaLucroId, string userId)
        {
            var categoria = await _crud.GetCategoriaLucroById(categoriaLucroId);
            if (categoria == null || categoria.UsuarioId != userId)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Categoria de Lucro não encontrada"));
                return null;
            }
            return new CategoriaLucroResponseDto
            {
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
            };
        }

        //Criar Categoria de Lucro
        public async Task<CategoriaLucroResponseDto> CriarCategoriaLucro(CategoriaLucroCreateDto dto, string userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
            {
                _notificador.Adicionar(new Notificacao.Notificacao("O nome da categoria de lucro é obrigatório"));
                return null;
            }
            

            var novaCategoria = new CategoriaLucro
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                UsuarioId = userId
            };

            var categoriaCriada = await _crud.CreateCategoriaLucro(novaCategoria);// salva no banco
            return new CategoriaLucroResponseDto
            {
                Nome = categoriaCriada.Nome,
                Descricao = categoriaCriada.Descricao,
            };
        }

        //Atualizar Categoria Lucro
        public async Task<CategoriaLucroResponseDto> AtualizarCategoriaLucro(CategoriaLucroCreateDto dto, Guid id)
        {
             var CategoriaLucro = await _crud.GetCategoriaLucroById(id);
            if (CategoriaLucro == null)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Categoria de lucro não encontrada"));
                return null;
            }

            var CategoriaAtualizada = await _crud.UpdateCategoriaLucro(CategoriaLucro);

            return new CategoriaLucroResponseDto
            {
                Nome = CategoriaAtualizada.Nome,
                Descricao = CategoriaAtualizada.Descricao,
            };
        }

        //Deletar Categoria Lucro
        public async Task<string> DeletarCategoriaLucro (Guid id)
        {
            var CategoriaLucro = await _crud.GetCategoriaLucroById(id);
            if (CategoriaLucro == null)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Categoria de lucro não encontrada"));
                return null;
            }
            await _crud.DeleteCategoriaLucro(CategoriaLucro);

            var resposta = "Categoria de lucro deletada com sucesso";

            return resposta;
        }



    }
}  
