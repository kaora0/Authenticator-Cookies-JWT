using Lab_7._1.Services;
using Lab_7._1.Endpoints.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Lab_7._1.Data;
using Lab_7._1.Interfaces;
using Lab_7.Repositories;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LearningDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<UserService>();

// Добавляем AutoMapper в контейнер сервисов.
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Добавляем маршрутизацию для эндпоинтов пользователей
app.MapUsersEndpoints();

app.Run();

public static class UserEndpoints
{
    public static void MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);
        app.MapPost("login", Login);
    }

    private static async Task<IResult> Register(
        [FromBody] RegisterUserRequest request,
        UserService usersService)
    {
        await usersService.Register(request.UserName, request.Email, request.Password);

        return Results.Ok();
    }

    private static async Task<IResult> Login(
        [FromBody] LoginUserRequest request,
        UserService usersService,
        HttpContext context)
    {
        //создание токена
        var token = await usersService.Login(request.Email, request.Password);

        // 
        context.Response.Cookies.Append("secretCookie", token);

        return Results.Ok(token);
    }
}