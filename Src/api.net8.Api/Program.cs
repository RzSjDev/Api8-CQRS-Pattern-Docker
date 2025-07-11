using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using api_net9.Loging;
using api_net9.Middleware;
using api_net9.Application;
using api_net9.Application.ProductFeature.Queries.FindProduct;
using api_net9.Application.Context;
using api_net9.Infrastructure.context;
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

