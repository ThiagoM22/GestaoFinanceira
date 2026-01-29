using GestaoFinanceira.DTOs.CategoriaGasto;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Services.CateGasto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoFinanceira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriasGastosController : MainController
    {
        private readonly ICateGastoService _services;

        public CategoriasGastosController(ICateGastoService services, INotificador notificador) : base(notificador)
        {
            _services = services;
        }

        //Listar Categoria de Gastos
        [HttpGet]
        public async Task<ActionResult<CategoriaGastoResponseDto>> ListarCategoriadeGastos()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var categorias = await _services.ListarCategoriasGasto(userId.ToString());
            return CustomResponse(categorias);

        }

        //Buscar Categoria de Gasto por Id
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaGastoResponseDto>> BuscarCategoriadeGastoPorId(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var categoria = await _services.BuscarCategoriaGastoPorId(id, userId.ToString());
            return CustomResponse(categoria);
        }

        //Criar Categoria de Gasto
        [HttpPost]
        public async Task<ActionResult<CategoriaGastoResponseDto>> CriarCategoriadeGasto([FromBody] CategoriaGastoCreateDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var categoriaCriada = await _services.CriarCategoriaGasto(dto, userId.ToString());
            return CustomResponse(categoriaCriada);
        }

        //Atualizar Categoria de Gasto
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaGastoResponseDto>> AtualizarCategoriadeGasto(Guid id, [FromBody] CategoriaGastoCreateDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var categoriaAtualizada = await _services.AtualizarCategoriaGasto(id, dto, userId.ToString());
            return CustomResponse(categoriaAtualizada);
        }

        //Deletar Categoria de Gasto
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletarCategoriadeGasto(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var resultado = await _services.DeletarCategoriaGasto(id, userId.ToString());
            return CustomResponse(resultado);
        }
    }
}
