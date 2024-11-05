using LivrariaSouza.DataAccess;
using LivrariaSouza.DataAccess.Repository.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ValoresServices>();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona suporte a cache e sessões
builder.Services.AddDistributedMemoryCache(); // Armazena dados de sessão na memória do servidor
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Expira após 30 minutos de inatividade
    options.Cookie.HttpOnly = true; // Bloqueia acesso via JavaScript (mais seguro)
    options.Cookie.IsEssential = true; // Faz com que o cookie funcione sem consentimento de cookies (se necessário)
});

var app = builder.Build();

// Habilita middleware de sessões
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Lida com erros na produção
    app.UseHsts(); // HSTS
}

// Configuração para redirecionar erros 404 para a ação HomePageCliente
app.UseStatusCodePagesWithReExecute("/Home/Error404");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
