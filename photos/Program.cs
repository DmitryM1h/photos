using Core.Configuration;
using Microsoft.OpenApi.Models;
using FluentValidation;
using Core.entities;
using Database.Configuration;
using Application.Configuration;
using System.Net.Http.Headers;
using Core.MiddleWare;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));


builder.Services.AddValidators();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddLogging();


var app = builder.Build();

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
