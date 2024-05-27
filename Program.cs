using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ITicket.Models;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<ContextoDb>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConexaoSQLite")));
//configuracao do contexto do banco de dados SQLite - ConexaoSQLite é o nome da conexao que foi nomeado no arquivo appsettings.json

// Add Identity services
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ContextoDb>();

builder.Services.AddDistributedMemoryCache();

//Serviços de sessao para manter o usuario conectado
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);//mantem o usuario conectado por 30 minutos afk
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Adiciona servico de email
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Logging.ClearProviders(); // Clear existing logging providers
builder.Logging.AddConsole(); // Log to console
builder.Logging.AddDebug(); // Log to debug output



var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

// Add these two middleware in this order
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "testeConexao",
    pattern: "account/testeconexao",
    defaults: new { controller = "Account", action = "TesteConexao" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();