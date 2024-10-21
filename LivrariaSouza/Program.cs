using LivrariaSouza.DataAccess;
using LivrariaSouza.DataAccess.Repository.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ValoresServices>(); // Serviço de valores
builder.Services.AddControllersWithViews(); // Adiciona suporte a controladores e views

// Configuração do banco de dados (por exemplo, usando SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Lida com erros na produção
    app.UseHsts(); // HSTS
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Ativa arquivos estáticos

app.UseRouting(); // Ativa roteamento

app.UseAuthorization(); // Ativa autorização

// Define rotas de controlador
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Home}/{action=RetornaTodosLivros}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Inicia o aplicativo
