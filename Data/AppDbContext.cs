using System;
using Microsoft.EntityFrameworkCore;
using BackendToyo.Models;

namespace BackendToyo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Token> Tokens { get; set; }
        public DbSet<TypeToken> TypeTokens { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TxTokenPlayer> TxsTokenPlayer { get; set; }
        public DbSet<Stat> Stats { get; set; }
        public DbSet<Parts> Parts { get; set; }
        public DbSet<Toyo> Toyos { get; set; }
        public DbSet<ToyoPlayer> ToyosPlayer { get; set; }
        public DbSet<PartPlayer> PartsPlayer { get; set; }
        public DbSet<SmartContractToyoMint> SmartContractToyoMints { get; set; }
        public DbSet<SmartContractToyoSync> SmartContractToyoSyncs { get; set; }
        public DbSet<SmartContractToyoTransfer> SmartContractToyoTransfers { get; set; }
        public DbSet<SmartContractToyoType> SmartContractToyoTypes { get; set; }
        public DbSet<SmartContractToyoSwap> SmartContractToyoSwaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<SmartContractToyoMint>()
                .HasKey(p => new { p.TransactionHash, p.TokenId, p.ChainId});
            
            modelBuilder.Entity<SmartContractToyoSync>()
                .HasKey(p => new { p.ChainId, p.EventName,p.ContractAddress });

            modelBuilder.Entity<SmartContractToyoTransfer>()
                .HasKey(p => new { p.TransactionHash, p.TokenId, p.ChainId});

            modelBuilder.Entity<SmartContractToyoType>()
                .HasKey(p => new { p.TransactionHash, p.TypeId, p.ChainId });

            modelBuilder.Entity<SmartContractToyoType>()
                .HasIndex(u => u.ChainId)
                .IsUnique(false);

            modelBuilder.Entity<SmartContractToyoSwap>()
                .HasKey(p => new { p.TransactionHash, p.FromTokenId, p.ToTokenId, p.ChainId});

            modelBuilder.Entity<Event>()
                .HasKey(p => p.Id);  

            modelBuilder.Entity<Player>()
                .HasKey(p => p.Id);                

            modelBuilder.Entity<TypeToken>()
                .HasKey(p => new { p.TypeId, p.ChainId });

            modelBuilder.Entity<Token>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<TypeToken>()
                .HasOne<SmartContractToyoType>()
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .HasPrincipalKey(p => p.TypeId);
           
            modelBuilder.Entity<TypeToken>()
                .HasOne<SmartContractToyoType>()
                .WithMany()
                .HasForeignKey(p => p.ChainId)
                .HasPrincipalKey(p => p.ChainId);

            modelBuilder.Entity<Token>()
                .HasOne<TypeToken>()
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .HasPrincipalKey(p => p.TypeId);

            modelBuilder.Entity<TxTokenPlayer>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<TxTokenPlayer>()
                .HasOne<Player>()
                .WithMany()
                .HasForeignKey(p => p.PlayerId)
                .HasPrincipalKey(p => p.Id);

            modelBuilder.Entity<TxTokenPlayer>()
                .HasOne<Token>()
                .WithMany()
                .HasForeignKey(p => p.TokenId)
                .HasPrincipalKey(p => p.Id);

            modelBuilder.Entity<Toyo>()
                .HasKey(p => p.Id);     
            
            modelBuilder.Entity<Stat>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Stat>().HasData(
                new { Id = 1, Name = "Vitality" },
                new { Id = 2, Name = "Strength" },
                new { Id = 3, Name = "Resistance" },
                new { Id = 4, Name = "CyberForce" },
                new { Id = 5, Name = "Resilience" },
                new { Id = 6, Name = "Precision" },
                new { Id = 7, Name = "Technique" },
                new { Id = 8, Name = "Analysis" },
                new { Id = 9, Name = "Speed" },
                new { Id = 10, Name = "Agility" },
                new { Id = 11, Name = "Stamina" },
                new { Id = 12, Name = "Luck" }
            );

            modelBuilder.Entity<Parts>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<PartPlayer>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<PartPlayer>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PartPlayer>()
                .HasOne<Parts>()
                .WithMany()
                .HasForeignKey(p => p.PartId)
                .HasPrincipalKey(p => p.Id);

            modelBuilder.Entity<PartPlayer>()
                .HasOne<Stat>()
                .WithMany()
                .HasForeignKey(p => p.StatId)
                .HasPrincipalKey(p => p.Id);

            modelBuilder.Entity<ToyoPlayer>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ToyoPlayer>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ToyoPlayer>()
                .HasOne<Toyo>()
                .WithMany()
                .HasForeignKey(p => p.ToyoId)
                .HasPrincipalKey(p => p.Id);
        }
    }
}