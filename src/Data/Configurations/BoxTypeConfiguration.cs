using BackendToyo.Models.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendToyo.Data.Configurations
{
    public class BoxTypeConfiguration : IEntityTypeConfiguration<BoxType>
    {
        public void Configure(EntityTypeBuilder<BoxType> builder)
        {
            builder
                .ToTable("BoxTypes")
                .HasKey(p => p.BoxTypeId)
                .HasName("pk_boxtypes");
            
            builder
                .HasOne<TypeToken>(p => p.TypeToken)
                .WithOne(tt => tt.BoxType)
                .IsRequired(false)
                .HasForeignKey<BoxType>(box => box.TypeId)
                .HasPrincipalKey<TypeToken>(token => token.Id);

            builder
                .Property(p => p.BoxTypeId)
                .HasColumnName("BoxTypeId")
                .HasColumnType("int")
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.TypeId)
                .HasColumnName("TypeId")
                .HasColumnType("int")
                .IsRequired();
            
            builder
                .Property(p => p.IsFortified)
                .HasColumnName("IsFortified")
                .HasColumnType("tinyint(1)")
                .IsRequired();

            builder
                .Property(p => p.IsJakana)
                .HasColumnName("IsJakana")
                .HasColumnType("tinyint(1)")
                .IsRequired();

            builder.HasData(
                new BoxType{BoxTypeId = 1, TypeId = 1, IsFortified = false, IsJakana = false},
                new BoxType{BoxTypeId = 2, TypeId = 2, IsFortified = true, IsJakana = false},
                new BoxType{BoxTypeId = 3, TypeId = 6, IsFortified = false, IsJakana = true},
                new BoxType{BoxTypeId = 4, TypeId = 7, IsFortified = true, IsJakana = true}
            );

        }
    }
}