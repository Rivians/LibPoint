using LibPoint.Infrastructure;
using LibPoint.Persistence;
using LibPoint.Application;
using LibPoint.Domain.Constants;
using LibPoint.Application.Abstractions;
using LibPoint.Persistence.Services;
using LibPoint.Application.Features.Review.Handlers;
using LibPoint.Persistence.Repositories;
using LibPoint.Application.Features.Books.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddScoped(typeof(IBookRepository<>), typeof(BookRepository<>));

builder.Services.AddScoped<RemoveReviewCommandHandler>();
builder.Services.AddScoped<UpdateReviewCommandHandler>();
builder.Services.AddScoped<CreateReviewCommandHandler>();
builder.Services.AddScoped<GetReviewQueryHandler>();
builder.Services.AddScoped<GetReviewByIdQueryHandler>();

builder.Services.AddScoped<RemoveBookCommandHandler>();
builder.Services.AddScoped<UpdateBookCommandHandler>();
builder.Services.AddScoped<CreateBookCommandHandler>();
builder.Services.AddScoped<GetBookQueryHandler>();
builder.Services.AddScoped<GetBookByIdQueryHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// persistence midw eklenecek..
app.UseInfrastructureMiddlewares();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
