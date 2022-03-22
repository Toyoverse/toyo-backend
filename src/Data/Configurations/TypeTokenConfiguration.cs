using BackendToyo.Models.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendToyo.Data.Configurations
{
    public class TypeTokenConfiguration : IEntityTypeConfiguration<TypeToken>
    {
        public void Configure(EntityTypeBuilder<TypeToken> builder)
        {
            builder.HasKey(p => new { p.Id, p.ChainId });
            
            builder.ToTable("TypeTokens");
            
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
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(50);
        }
    }
}