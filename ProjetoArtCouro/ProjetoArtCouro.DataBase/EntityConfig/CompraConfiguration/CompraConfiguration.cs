﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Models.Compras;

namespace ProjetoArtCouro.DataBase.EntityConfig.CompraConfiguration
{
    public class CompraConfiguration : EntityTypeConfiguration<Compra>
    {
        public CompraConfiguration()
        {
            ToTable("Compra");

            Property(x => x.CompraId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.CompraCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            Property(x => x.StatusCompra)
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.ValorTotalBruto)
                .IsRequired()
                .HasColumnType("decimal");

            Property(x => x.ValorTotalFrete)
                .HasColumnType("decimal");

            Property(x => x.ValorTotalLiquido)
                .IsRequired()
                .HasColumnType("decimal");

            //Relacionamento 0 ou 1 : N
            HasRequired(x => x.Usuario)
                .WithMany(x => x.Compras);

            //Relacionamento 1 : N
            HasOptional(x => x.Fornecedor)
                .WithMany(x => x.Compras);

            //Relacionamento 0 ou 1 : N
            HasOptional(x => x.CondicaoPagamento)
                .WithMany(x => x.Compras);

            //Relacionamento 0 ou 1 : N
            HasOptional(x => x.FormaPagamento)
                .WithMany(x => x.Compras);

            //Relacionamento 1 : N
            HasMany(x => x.ItensCompra)
                .WithRequired(x => x.Compra)
                .WillCascadeOnDelete(true);

            //Relacionamento 1 : 0 ou N
            HasMany(x => x.ContasPagar)
                .WithOptional(x => x.Compra)
                .WillCascadeOnDelete(true);
        }
    }
}
