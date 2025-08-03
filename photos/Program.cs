using Microsoft.OpenApi.Models;
using Database.Configuration;
using Application.Configuration;
using Auth;
using Core.MiddleWare;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));




builder.Services.AddAuthorization();

builder.Services.AddAuth();
    
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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
