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
            
            builder.ToTable("tb_type_token");
            
            builder.Property(p => p.Id)
                .HasColumnName("id_type_token")
                .HasColumnType("int");
            
            builder.Property(p => p.ChainId)
                .HasColumnName("id_chain")
                .HasColumnType("varchar")
                .HasMaxLength(10);

            builder.Property(p => p.Type)
                .HasColumnName("ds_type_token")
                .HasColumnType("varchar")
                .HasMaxLength(20);

            builder.Property( p=> p.Name)
                .HasColumnName("tx_name")
                .HasColumnType("varchar")
                .HasMaxLength(50);
        }
    }
}