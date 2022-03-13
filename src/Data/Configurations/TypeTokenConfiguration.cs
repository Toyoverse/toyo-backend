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
                .HasColumnName("TypeId")
                .HasColumnType("int");
            
            builder.Property(p => p.ChainId)
                .HasColumnName("ChainId")
                .HasColumnType("varchar")
                .HasMaxLength(10);

            builder.Property(p => p.Type)
                .HasColumnName("Type")
                .HasColumnType("varchar")
                .HasMaxLength(20);

            builder.Property( p=> p.Name)
                .HasColumnType("varchar")
                .HasColumnName("Name")
                .HasMaxLength(50);
        }
    }
}