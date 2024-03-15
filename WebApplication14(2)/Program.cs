var builder = WebApplication.CreateBuilder();

builder.Services.AddCors(options => {                       //добавляем сервисы CORS

    options.AddPolicy("AllowTestSite", builder => builder
        .WithOrigins("https://localhost:7044") // принимаются запросы только с определенных адресов
        .AllowAnyHeader() // принимаются запросы с любыми заголовками
        .AllowAnyMethod());// принимаются запросы любого типа (GET/POST)

    options.AddPolicy("AllowNormSite", builder => builder
            .WithOrigins("https://localhost.com") // принимаются запросы только с определенных адресов
            .AllowAnyHeader()
            .AllowAnyMethod());
});
    
var app = builder.Build();

app.UseCors();// используем CORS

app.MapGet("/", async context => await context.Response.WriteAsync("Hello World!"))
    .RequireCors(options => options.AllowAnyOrigin());//установливаем настройки CORS для каждого конкретного маршрута

app.MapGet("/home", async context => await context.Response.WriteAsync("Home Page!"))
    .RequireCors("AllowNormSite");//установливаем настройки CORS для каждого конкретного маршрута

app.MapGet("/about", async context => await context.Response.WriteAsync("About Page!"))
    .RequireCors("AllowTestSite");//установливаем настройки CORS для каждого конкретного маршрута

app.Run();