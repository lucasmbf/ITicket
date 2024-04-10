using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ITicket.Models;
using System.Configuration;



var builder = WebApplication.CreateBuilder(args);



// Adicione outros serviços ao contêiner
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ContextoDb>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConexaoSQLite")));
//configuracao do contexto do banco de dados SQLite - ConexaoSQLite é o nome da conexao que foi nomeado no arquivo appsettings.json



var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "testeConexao",
    pattern: "account/testeconexao",
    defaults: new { controller = "Account", action = "TesteConexao" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();
