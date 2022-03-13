using BackendToyo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendToyo.Data.EntityConfigurations
{
    public class TypeTokenConfiguration : IEntityTypeConfiguration<TypeToken>
    {
        public void Configure(EntityTypeBuilder<TypeToken> builder)
        {
            builder.HasKey(p => new { p.Id, p.ChainId });

            builder.HasOne<SmartContractToyoType>()
                .WithMany()
                .HasForeignKey(p => p.Id)
                .HasPrincipalKey(p => p.TypeId);
           
            builder.HasOne<SmartContractToyoType>()
                .WithMany()
                .HasForeignKey(p => p.ChainId)
                .HasPrincipalKey(p => p.ChainId);

            builder.Property(p => p.Id)
                .HasColumnName("id_typetoken")
                .HasColumnType("int");
            
            builder.Property(p => p.ChainId)
                .HasColumnName("id_chain")
                .HasColumnType("varchar")
                .HasMaxLength(10);

            builder.Property(p => p.Type)
                .HasColumnName("ds_typetoken")
                .HasColumnType("varchar")
                .HasMaxLength(20);

            builder.Property( p=> p.Name)
                .HasColumnName("nm_typetoken")
                .HasColumnType("varchar")
                .HasMaxLength(50);
        }
    }
}