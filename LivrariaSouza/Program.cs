using LivrariaSouza.DataAccess;
using LivrariaSouza.DataAccess.Repository.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ValoresServices>(); // Servi�o de valores
builder.Services.AddControllersWithViews(); // Adiciona suporte a controladores e views

// Configura��o do banco de dados (por exemplo, usando SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Lida com erros na produ��o
    app.UseHsts(); // HSTS
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Ativa arquivos est�ticos

app.UseRouting(); // Ativa roteamento

app.UseAuthorization(); // Ativa autoriza��o

// Define rotas de controlador
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Home}/{action=RetornaTodosLivros}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Inicia o aplicativo
