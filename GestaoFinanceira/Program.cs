using GestaoFinanceira.Data;
using GestaoFinanceira.Models;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Repositories.CateGasto;
using GestaoFinanceira.Repositories.CateLucro;
using GestaoFinanceira.Repositories.Usuario;
using GestaoFinanceira.Services.CateGasto;
using GestaoFinanceira.Services.CateLucro;
using GestaoFinanceira.Services.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurando o DbContext com SQL Server usando a string de conexão do appsettings.json
builder.Services.AddDbContext<ApiDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICateLucroRepository, CateLucroRepository>();
builder.Services.AddScoped<ICateGastoRepository, CateGastoRepository>();
builder.Services.AddScoped<ICateGastoService, CateGastoService>();
builder.Services.AddScoped<ICateLucroService, CateLucroService>();
builder.Services.AddScoped<INotificador, Notificador>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGroup("/Usuario").MapIdentityApi<Usuario>(); // Mapeia os endpoints de API do Identity para a entidade Usuario

app.Run();
