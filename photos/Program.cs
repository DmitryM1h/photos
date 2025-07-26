using Core.Configuration;
using Microsoft.OpenApi.Models;
using FluentValidation;
using Core.entities;
using Database.Configuration;
using Application.Configuration;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers(opt =>
{
    opt.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    opt.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");

}
).AddXmlSerializerFormatters()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

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
