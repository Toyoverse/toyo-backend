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

            modelBuilder.Entity<SmartContractToyoSwap>()
                .HasKey(p => new { p.TransactionHash, p.TokenId, p.ChainId});

            modelBuilder.Entity<Event>()
                .HasKey(p => p.Id);  

            modelBuilder.Entity<Player>()
                .HasKey(p => p.Id);                

            modelBuilder.Entity<TypeToken>()
                .HasKey(p => p.Id);     

            modelBuilder.Entity<Token>()
                .HasKey(p => p.Id);
           
            modelBuilder.Entity<Token>()
                .HasOne<TypeToken>()
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .HasPrincipalKey(p => p.Id);

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

            modelBuilder.Entity<Parts>()
                .HasKey(p => p.Id);     


        }
    }
}