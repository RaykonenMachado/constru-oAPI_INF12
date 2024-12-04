using Microsoft.EntityFrameworkCore;
using construcaoAPI_INF12.Modelos;
using construcaoAPI_INF12.Api.Modelos;

namespace construcaoAPI_INF12.Data
{
    public class LojaClientesDbContext : DbContext
    {
        public LojaClientesDbContext(DbContextOptions<LojaClientesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItensPedido> ItensPedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>()
                .HasOne<Cliente>()
                .WithMany(c => c.Pedidos)
                .HasForeignKey(f => f.idPedido)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pedido>()
                .HasMany<ItensPedido>()
                .WithOne(ip => ip.Pedido)
                .HasForeignKey(f => f.idPedido);

            modelBuilder.Entity<ItensPedido>()
                .HasOne(ip => ip.Pedido)
                .WithMany(ip => ip.itensPedidos)
                .HasForeignKey(f => f.idPedido)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Produto>()
                .HasMany<ItensPedido>()
                .WithOne(p => p.Produto)
                .HasForeignKey(f => f.idProduto)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "Server=localhost;Port=3306;Database=meu_banco;User=root;Password=minha_senha;",
                    new MySqlServerVersion(new Version(8, 0, 27)) // Substitua pela sua versão do MySQL
                );
            }
        }
    }
}