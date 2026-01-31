using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Services.CateLucro;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GestaoFinanceira.DTOs.CategoriaLucro;
using Microsoft.AspNetCore.Authorization;

namespace GestaoFinanceira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriasLucrosController : MainController
    {
        private readonly ICateLucroService _service;

        public CategoriasLucrosController(ICateLucroService service, INotificador notificador) : base(notificador)
        {
            _service = service;
        }

        //Listar Categorias de Lucro
        [HttpGet]
        public async Task<ActionResult> GetCategoriasLucro()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var categorias = await _service.ListarCategoriasLucro(userId);
            return CustomResponse(categorias);
        }

        //Buscar Categoria de Lucro por Id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoriaLucroPorId(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var categoria = await _service.BuscarCategoriaLucroPorId(id, userId);
            return CustomResponse(categoria);
        }

        //Criar Categoria de Lucro
        [HttpPost]
        public async Task<ActionResult> CriarCategoriaLucro([FromBody] CategoriaLucroCreateDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var categoriaCriada = await _service.CriarCategoriaLucro(dto, userId.ToString());
            return CustomResponse(categoriaCriada);
        }

        //Atualizar Categoria de Lucro
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarCategoriaLucro(Guid id, [FromBody] CategoriaLucroCreateDto dto)
        {
            var categoriaAtualizada = await _service.AtualizarCategoriaLucro(dto, id);
            return CustomResponse(categoriaAtualizada);
        }

        //Deletar Categoria de Lucro
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarCategoriaLucro(Guid id)
        {
            var resultado = await _service.DeletarCategoriaLucro(id);
            return CustomResponse(resultado);
        }
    }
}
