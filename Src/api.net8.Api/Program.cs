using Src.api_net8.Application;
using Src.api_.net8.Common.Enum;
using Src.api_net8.Application.ProductFeature.Queries.GetAllProduct;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Src.api.net8.Api.Loging;
using Src.api.net8.Api.Middleware;
using Src.api_net8.Application.Context;
using Src.api_net8.Application.ProductFeature.Queries.FindProduct;
using Src.api_net8.Infrastructure.context;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myDb")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IDataContext, DataContext>();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(FindProductQuery).Assembly));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();

