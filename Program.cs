using System.Text.Json.Serialization;
using GestaoObrigacoes.Dados;  // Este namespace deve corresponder ao do contexto
using GestaoObrigacoes.Servicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Carregando variáveis de ambiente do arquivo .env (se existir)
try {
    DotNetEnv.Env.Load();
} catch {
    // Continuar mesmo se o arquivo .env não existir
    Console.WriteLine("Arquivo .env não encontrado. Usando configurações padrão.");
}

// Adicionando serviços ao container
builder.Services.AddControllers()
    .AddJsonOptions(opcoes =>
    {
        opcoes.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opcoes.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Configurando Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "API de Obrigações Acessórias", 
        Version = "v1",
        Description = "API para gerenciamento de empresas e obrigações acessórias"
    });
});

// Carregar string de conexão do .env
var connectionString = Environment.GetEnvironmentVariable("SUPABASE_CONNECTION_STRING");

// Configurar banco de dados
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("String de conexão com o banco de dados não configurada.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrando serviços
builder.Services.AddScoped<ServicoEmpresa>();
builder.Services.AddScoped<ServicoObrigacao>();

var app = builder.Build();

// Configurando o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Criar/atualizar banco de dados
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();