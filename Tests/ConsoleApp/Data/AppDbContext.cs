using ConsoleAppTest.Data.Configurations;
using ConsoleAppTest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest.Data
{
    public class AppDbContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());


        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                //.UseMySQL("Server=mysql10-farm15.uni5.net;Port=3306;Database=gdac04;Uid=gdac04;Pwd=JemS2023;",p => p.EnableRetryOnFailure());
                .UseMySQL(
                "Server=mysql10-farm15.uni5.net;Port=3306;Database=gdac04;Uid=gdac04;Pwd=JemS2023;",
                p => p.EnableRetryOnFailure(
                    maxRetryCount: 2, 
                    maxRetryDelay: TimeSpan.FromSeconds(5), 
                    errorNumbersToAdd: null)
                .MigrationsHistoryTable("TabelaMigrationCore"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            MapearPropriedadesEsquecidas(modelBuilder);

            //modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            //modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            //modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            //modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());

            //modelBuilder.Entity<Cliente>(p =>
            //{
            //    p.ToTable("Clientes");
            //    p.HasKey(p => p.Id);
            //    p.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            //    p.Property(p => p.Telefone).HasColumnType("VARCHAR(11)");
            //    p.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
            //    p.Property(p => p.Estado).HasColumnType("CHAR(2)").IsRequired();
            //    p.Property(p => p.Cidade).HasMaxLength(60).IsRequired();

            //    p.HasIndex(p => p.Telefone).HasName("idx_cliente_telefone");
            //});

            //modelBuilder.Entity<Produto>(p =>
            //{
            //    p.ToTable("Produtos");
            //    p.HasKey(p => p.Id);
            //    p.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            //    p.Property(p => p.Descricao).HasColumnType("VARCHAR(60)");
            //    p.Property(p => p.Valor).IsRequired();
            //    p.Property(p => p.TipoProduto).HasConversion<string>();
            //});

            //modelBuilder.Entity<Pedido>(p =>
            //{
            //    p.ToTable("Pedidos");
            //    p.HasKey(p => p.Id);
            //    p.Property(p => p.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            //    p.Property(p => p.Status).HasConversion<string>();
            //    p.Property(p => p.TipoFrete).HasConversion<int>();
            //    p.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

            //    p.HasMany(p => p.Itens)
            //        .WithOne(p => p.Pedido)
            //        .OnDelete(DeleteBehavior.Cascade);
            //});

            //modelBuilder.Entity<PedidoItem>(p =>
            //{
            //    p.ToTable("PedidosItens");
            //    p.HasKey(p => p.Id);
            //    p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
            //    p.Property(p => p.Valor).IsRequired();
            //    p.Property(p => p.Desconto).IsRequired();
            //});

        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var propriedades = entityType.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach (var propriedade in propriedades)
                {
                    if (string.IsNullOrEmpty(propriedade.GetColumnType()) && !propriedade.GetMaxLength().HasValue)
                    {
                        //propriedade.SetMaxLength(60);
                        propriedade.SetColumnType("VARCHAR(60)");
                    }
                }
            }
        }
    }
}
