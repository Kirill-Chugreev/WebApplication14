var builder = WebApplication.CreateBuilder();

builder.Services.AddCors(options => {                       //��������� ������� CORS

    options.AddPolicy("AllowTestSite", builder => builder
        .WithOrigins("https://localhost:7044") // ����������� ������� ������ � ������������ �������
        .AllowAnyHeader() // ����������� ������� � ������ �����������
        .AllowAnyMethod());// ����������� ������� ������ ���� (GET/POST)

    options.AddPolicy("AllowNormSite", builder => builder
            .WithOrigins("https://localhost.com") // ����������� ������� ������ � ������������ �������
            .AllowAnyHeader()
            .AllowAnyMethod());
});
    
var app = builder.Build();

app.UseCors();// ���������� CORS

app.MapGet("/", async context => await context.Response.WriteAsync("Hello World!"))
    .RequireCors(options => options.AllowAnyOrigin());//������������� ��������� CORS ��� ������� ����������� ��������

app.MapGet("/home", async context => await context.Response.WriteAsync("Home Page!"))
    .RequireCors("AllowNormSite");//������������� ��������� CORS ��� ������� ����������� ��������

app.MapGet("/about", async context => await context.Response.WriteAsync("About Page!"))
    .RequireCors("AllowTestSite");//������������� ��������� CORS ��� ������� ����������� ��������

app.Run();