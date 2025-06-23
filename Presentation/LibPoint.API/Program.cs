using LibPoint.Infrastructure;
using LibPoint.Persistence;
using LibPoint.Application;
using LibPoint.Domain.Constants;
using Microsoft.OpenApi.Models;
using System.Reflection;
using LibPoint.Infrastructure.Hubs;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(8080);
//});

//builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();

builder.Services.AddRouting(opt =>
{
    opt.LowercaseUrls = true;
});

//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibPoint", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
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
            new string[] {}
        }
    });
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .SetIsOriginAllowed(_ => true)
                           .AllowCredentials();
        });
});

var app = builder.Build();

app.UseCors("AllowOrigin");

app.UseSwagger();

app.UseSwaggerUI();

// persistence midw eklenecek..
app.UseInfrastructureMiddlewares();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapHub<SeatHub>("/seatHub");

app.MapControllers();

app.Run();
