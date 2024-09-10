using _0411.Contracts;
using _0411.Data;
using _0411.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Write Serilog events to files in text and JSON formats https://github.com/serilog/serilog-sinks-file
var logger = new LoggerConfiguration().WriteTo.File(
    new CompactJsonFormatter(),
    "logs/log.json",
    rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: null,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning
    )
    .CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}
                    }
                });
});

string? tokenKeyString = builder.Configuration.GetSection("AppSettings:TokenKey").Value;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    tokenKeyString != null ? tokenKeyString : ""
                )),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


builder.Services.AddDbContext<DataContext>(optionsBuilder => optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), optionsBuilder => optionsBuilder.EnableRetryOnFailure()));

builder.Services.AddScoped(typeof(IGenericoRepository<>), typeof(GenericoRepository<>));
builder.Services.AddScoped<ITipoQuartoRepository, TipoQuartoRepository>();
builder.Services.AddScoped<IQuartoRepository, QuartoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IConsumoRepository, ConsumoRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAutenticacaoRepository, AutenticacaoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();