using Microsoft.OpenApi.Models;
using Database.Configuration;
using Application.Configuration;
using Core.MiddleWare;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));



builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddLogging();


var app = builder.Build();


app.UseMiddleware<HttpRequestLoggerMiddleWare>();

app.UseMiddleware<ExceptionHandlingMiddleware>();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
