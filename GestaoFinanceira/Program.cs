using FluentValidation;
using FluentValidation.AspNetCore;
using GestaoFinanceira.Data;
using GestaoFinanceira.Models;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Repositories.CateGasto;
using GestaoFinanceira.Repositories.CateLucro;
using GestaoFinanceira.Repositories.Gastos;
using GestaoFinanceira.Repositories.Lucros;
using GestaoFinanceira.Repositories.Usuario;
using GestaoFinanceira.Services.CateGasto;
using GestaoFinanceira.Services.CateLucro;
using GestaoFinanceira.Services.Comparacao;
using GestaoFinanceira.Services.Gastos;
using GestaoFinanceira.Services.Lucros;
using GestaoFinanceira.Services.Resumo;
using GestaoFinanceira.Services.Usuario;
using GestaoFinanceira.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configuração do Identity com a classe personalizada Usuario
builder.Services.AddIdentityApiEndpoints<Usuario>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApiDbContext>() // Usando o ApiDbContext para armazenar os dados do Identity
.AddDefaultTokenProviders(); // Adiciona provedores de token padrão para funcionalidades como recuperação de senha e confirmação de email

// Configuração do Swagger para documentação da API
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new() { Title = "GestaoFinanceira API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    });

// Sempre ao criar um novo repository ou service adicionar ao scopo da aplicação no program

// Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICateLucroRepository, CateLucroRepository>();
builder.Services.AddScoped<ICateGastoRepository, CateGastoRepository>();
builder.Services.AddScoped<ILucroRepository, LucroRepository>();
builder.Services.AddScoped<IGastoRepository, GastoRepository>();

// Serviços
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IResumoService, ResumoService>();
builder.Services.AddScoped<IGastoService, GastoService>();
builder.Services.AddScoped<ILucroService, LucroService>();
builder.Services.AddScoped<ICateGastoService, CateGastoService>();
builder.Services.AddScoped<ICateLucroService, CateLucroService>();
builder.Services.AddScoped<IComparacaoService, ComparacaoService>();

// Notificação
builder.Services.AddScoped<INotificador, Notificador>();

//Validators
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoriaGastoCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoriaLucroCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GastoCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LucroCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioUpdateNameDtoValidator>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation(); // Habilita a validação automática do FluentValidation para os modelos de entrada (DTOs) nas ações dos controladores
builder.Services.AddValidatorsFromAssemblyContaining<Program>(); // Registra os validadores do FluentValidation a partir do assembly atual (Program.cs)

// Configura a resposta personalizada para erros de validação do modelo
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var erros = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .SelectMany(e => e.Value!.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        return new BadRequestObjectResult(new
        {
            sucesso = false,
            erros
        });
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/index.html", "Gestão Financeira");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGroup("/Usuario").MapIdentityApi<Usuario>(); // Mapeia os endpoints de API do Identity para a entidade Usuario

app.Run();
