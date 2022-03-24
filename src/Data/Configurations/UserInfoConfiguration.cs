using BackendToyo.Models.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendToyo.Data.Configurations
{
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasKey(p => p.Login).HasName("pk_login");

            builder.ToTable("tb_users");

            builder.Property(p => p.Login)
                .HasColumnName("id_login")
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Password)
                .HasColumnName("tx_password")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property( p => p.Role)
                .HasColumnName("tx_role")
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired();

            builder.HasData(
                new UserInfo{
                    Login = "sync_service", 
                    Password = "6fef533d07d5c11e14260529d9cea67978c31aee5d6e84f575f1dc95467dabbd",
                    Role = "Block Chain Service"
                    }
            );
        }
    }
}