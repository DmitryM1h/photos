using Core.Configuration;
using Microsoft.OpenApi.Models;
using Application;
using FluentValidation;
using Core.entities;
using Database.Configuration;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));


builder.Services.AddValidators();
builder.Services.AddInfrastructureServices(builder.Configuration);


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
