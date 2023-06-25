using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyShushiSiteApi.Core.Authorization;
using MyShushiSiteApi.DbModels;

string versionApp = "ver. 1.0 (от 2023.05.06)";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Для встраивания функциональности JWT-токенов в конвейер обработки запроса
//используется компонент JwtBearerAuthenticationMiddleware
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        //options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,  // укзывает, будет ли валидироваться издатель при валидации токена
            ValidIssuer = AuthOptions.ISSUER, // строка, представляющая издателя

            ValidateAudience = true, // будет ли валидироваться потребитель токена
            ValidAudience = AuthOptions.AUDIENCE, // установка потребителя токена

            ValidateIssuerSigningKey = true, // валидация ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),  // установка ключа безопасности

            ValidateLifetime = true, //будет ли валидироваться время существования
        };
    });

builder.Services.AddDbContext<MyDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("POSTGRESQL_SUSHI_CONNECTION"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "Shushi House";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"WebApi {versionApp}");
    });
}
//тест для запросов с других портов локально
app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
