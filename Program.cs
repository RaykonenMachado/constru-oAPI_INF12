using construcaoAPI_INF12.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql;
using construcaoAPI_INF12.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<LojaClientesDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("LojaClienteDb"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("LojaClienteDb"))));

        builder.Services.AddControllers();


        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (var e = app.Services.CreateScope())
        {
            var banco = e.ServiceProvider.GetRequiredService<LojaClientesDbContext>();

            banco.Database.Migrate();
            InicializarDados.Semear(banco);
        }

        if (app.Environment.IsDevelopment())
        {
             app.UseSwagger();
             app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
