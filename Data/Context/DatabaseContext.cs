using Fiap.Api.Alunos.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Alunos.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<RepresentanteModel> Representantes { get; set; }
        public virtual DbSet<ClienteModel> Clientes { get; set; }
        public virtual DbSet<ProdutoModel> Produtos { get; set; }
        public virtual DbSet<LojaModel> Lojas { get; set; }
        public virtual DbSet<PedidoModel> Pedidos { get; set; }
        public virtual DbSet<FornecedorModel> Fornecedores { get; set; }
        public virtual DbSet<PedidoProdutoModel> PedidoProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Representante
            modelBuilder.Entity<RepresentanteModel>(entity =>
            {
                entity.ToTable("Representantes");
                entity.HasKey(e => e.RepresentanteId);
                entity.Property(e => e.NomeRepresentante).IsRequired();
                entity.HasIndex(e => e.Cpf).IsUnique();
            });

            //Cliente
            modelBuilder.Entity<ClienteModel>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(e => e.ClienteId);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.DataNascimento).HasColumnType("DATE");
                entity.Property(e => e.Observacao).HasMaxLength(500);

                entity.HasOne(e => e.Representante)
                    .WithMany()
                    .HasForeignKey(e => e.RepresentanteId)
                    .IsRequired();
            });

            //Produto
            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("Produtos");
                entity.HasKey(e => e.ProdutoId);
                entity.Property(p => p.Nome).IsRequired();
                entity.Property(p => p.Descricao).IsRequired();
                entity.Property(p => p.Preco).HasColumnType("NUMBER(18,2)");

                entity.HasOne(p => p.Fornecedor)
                .WithMany(f => f.Produtos)
                .HasForeignKey(p => p.FornecedorId);
            });

            //Loja
            modelBuilder.Entity<LojaModel>(entity =>
            {
                entity.ToTable("Lojas");
                entity.HasKey(e => e.LojaId);
                entity.Property(e => e.LojaNome).IsRequired();
                entity.Property(e => e.Endereco).IsRequired();

                entity.HasMany(e => e.Pedidos)
                .WithOne(l => l.Loja)
                .HasForeignKey(k => k.LojaId);
            });

            //Pedido
            modelBuilder.Entity<PedidoModel>(entity =>
            {
                entity.ToTable("Pedidos");
                entity.HasKey(k => k.PedidoId);
                entity.Property(p => p.DataPedido).HasColumnType("DATE");

                entity.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(fk => fk.ClientId);

                entity.HasMany(p => p.PedidoProdutos)
                .WithOne(po => po.Pedido)
                .HasForeignKey(fk => fk.PedidoId);
            });

            //Fornecedor
            modelBuilder.Entity<FornecedorModel>(entity =>
            {
                entity.ToTable("Fornecedores");
                entity.HasKey(k => k.FornecedorId);
                entity.Property(n => n.Nome).IsRequired();
            });

            modelBuilder.Entity<PedidoProdutoModel>(entity =>
            {
                entity.HasKey(p => new 
                { 
                    p.PedidoId, 
                    p.ProdutoId 
                });

                entity.HasOne(po => po.Pedido)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(k => k.PedidoId);

                entity.HasOne(po => po.Produto)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(k => k.ProdutoId);
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }
    }
}
