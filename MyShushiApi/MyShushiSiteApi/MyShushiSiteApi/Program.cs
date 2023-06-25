using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyShushiSiteApi.Core.Authorization;
using MyShushiSiteApi.DbModels;

string versionApp = "ver. 1.0 (�� 2023.05.06)";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//��� ����������� ���������������� JWT-������� � �������� ��������� �������
//������������ ��������� JwtBearerAuthenticationMiddleware
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        //options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,  // ��������, ����� �� �������������� �������� ��� ��������� ������
            ValidIssuer = AuthOptions.ISSUER, // ������, �������������� ��������

            ValidateAudience = true, // ����� �� �������������� ����������� ������
            ValidAudience = AuthOptions.AUDIENCE, // ��������� ����������� ������

            ValidateIssuerSigningKey = true, // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),  // ��������� ����� ������������

            ValidateLifetime = true, //����� �� �������������� ����� �������������
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
//���� ��� �������� � ������ ������ ��������
app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
