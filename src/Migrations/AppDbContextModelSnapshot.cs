﻿// <auto-generated />
using System;
using BackendToyo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackendToyo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("BackendToyo.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CreateTimeStamp")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("BackendToyo.Models.PartPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BonusStat")
                        .HasColumnType("int");

                    b.Property<string>("ChainId")
                        .HasColumnType("longtext");

                    b.Property<int>("PartId")
                        .HasColumnType("int");

                    b.Property<int>("StatId")
                        .HasColumnType("int");

                    b.Property<int>("TokenId")
                        .HasColumnType("int");

                    b.Property<string>("WalletAddress")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PartId");

                    b.HasIndex("StatId");

                    b.ToTable("PartsPlayer");
                });

            modelBuilder.Entity("BackendToyo.Models.Parts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Colors")
                        .HasColumnType("int");

                    b.Property<int>("Cyber")
                        .HasColumnType("int");

                    b.Property<string>("Desc")
                        .HasColumnType("longtext");

                    b.Property<int>("Part")
                        .HasColumnType("int");

                    b.Property<string>("Prefix")
                        .HasColumnType("longtext");

                    b.Property<int>("RetroBone")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("TorsoId")
                        .HasColumnType("int");

                    b.Property<int>("Variants")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("BackendToyo.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("JoinTimeStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Mail")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("User")
                        .HasColumnType("longtext");

                    b.Property<string>("WalletAddress")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BackendToyo.Models.SmartContractToyoMint", b =>
                {
                    b.Property<string>("TransactionHash")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TokenId")
                        .HasColumnType("int");

                    b.Property<string>("ChainId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("BlockNumber")
                        .HasColumnType("int");

                    b.Property<long>("Gwei")
                        .HasColumnType("bigint");

                    b.Property<string>("Sender")
                        .HasColumnType("longtext");

                    b.Property<int>("TotalSypply")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("WalletAddress")
                        .HasColumnType("longtext");

                    b.HasKey("TransactionHash", "TokenId", "ChainId");

                    b.ToTable("SmartContractToyoMints");
                });

            modelBuilder.Entity("BackendToyo.Models.SmartContractToyoSwap", b =>
                {
                    b.Property<string>("TransactionHash")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("FromTokenId")
                        .HasColumnType("int");

                    b.Property<int>("ToTokenId")
                        .HasColumnType("int");

                    b.Property<string>("ChainId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("BlockNumber")
                        .HasColumnType("int");

                    b.Property<int>("FromTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Sender")
                        .HasColumnType("longtext");

                    b.Property<int>("ToTypeId")
                        .HasColumnType("int");

                    b.HasKey("TransactionHash", "FromTokenId", "ToTokenId", "ChainId");

                    b.ToTable("SmartContractToyoSwaps");
                });

            modelBuilder.Entity("BackendToyo.Models.SmartContractToyoSync", b =>
                {
                    b.Property<string>("ChainId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EventName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ContractAddress")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("LastBlockNumber")
                        .HasColumnType("int");

                    b.HasKey("ChainId", "EventName", "ContractAddress");

                    b.ToTable("SmartContractToyoSyncs");
                });

            modelBuilder.Entity("BackendToyo.Models.SmartContractToyoTransfer", b =>
                {
                    b.Property<string>("TransactionHash")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TokenId")
                        .HasColumnType("int");

                    b.Property<string>("ChainId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("BlockNumber")
                        .HasColumnType("int");

                    b.Property<string>("WalletAddress")
                        .HasColumnType("longtext");

                    b.HasKey("TransactionHash", "TokenId", "ChainId");

                    b.ToTable("SmartContractToyoTransfers");
                });

            modelBuilder.Entity("BackendToyo.Models.SmartContractToyoType", b =>
                {
                    b.Property<string>("TransactionHash")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("ChainId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("BlockNumber")
                        .HasColumnType("int");

                    b.Property<string>("MetaDataUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Sender")
                        .HasColumnType("longtext");

                    b.HasKey("TransactionHash", "TypeId", "ChainId");

                    b.HasIndex("ChainId");

                    b.ToTable("SmartContractToyoTypes");
                });

            modelBuilder.Entity("BackendToyo.Models.Stat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Stats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Vitality"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Strength"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Resistance"
                        },
                        new
                        {
                            Id = 4,
                            Name = "CyberForce"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Resilience"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Precision"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Technique"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Analysis"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Speed"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Agility"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Stamina"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Luck"
                        });
                });

            modelBuilder.Entity("BackendToyo.Models.Token", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("NFTId")
                        .HasColumnType("bigint");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("BackendToyo.Models.Toyo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BodyType")
                        .HasColumnType("int");

                    b.Property<int>("Colors")
                        .HasColumnType("int");

                    b.Property<int>("Cyber")
                        .HasColumnType("int");

                    b.Property<string>("Desc")
                        .HasColumnType("longtext");

                    b.Property<int>("Existe")
                        .HasColumnType("int");

                    b.Property<int>("Material")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<string>("Region")
                        .HasColumnType("longtext");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<string>("Thumb")
                        .HasColumnType("longtext");

                    b.Property<int>("Variants")
                        .HasColumnType("int");

                    b.Property<string>("Video")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Toyos");
                });

            modelBuilder.Entity("BackendToyo.Models.ToyoPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<int>("Analysis")
                        .HasColumnType("int");

                    b.Property<string>("ChainId")
                        .HasColumnType("longtext");

                    b.Property<bool>("ChangeValue")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("CyberForce")
                        .HasColumnType("int");

                    b.Property<int>("Luck")
                        .HasColumnType("int");

                    b.Property<int>("Precision")
                        .HasColumnType("int");

                    b.Property<int>("Resilience")
                        .HasColumnType("int");

                    b.Property<int>("Resistance")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<int>("Stamina")
                        .HasColumnType("int");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int>("Technique")
                        .HasColumnType("int");

                    b.Property<int>("TokenId")
                        .HasColumnType("int");

                    b.Property<int>("ToyoId")
                        .HasColumnType("int");

                    b.Property<int>("Vitality")
                        .HasColumnType("int");

                    b.Property<string>("WalletAddress")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ToyoId");

                    b.ToTable("ToyosPlayer");
                });

            modelBuilder.Entity("BackendToyo.Models.TxTokenPlayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("BlockNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("ChainId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TokenId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ToyoSku")
                        .HasColumnType("longtext");

                    b.Property<string>("TxHash")
                        .HasColumnType("longtext");

                    b.Property<string>("TxTimeStamp")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TokenId");

                    b.ToTable("TxsTokenPlayer");
                });

            modelBuilder.Entity("BackendToyo.Models.TypeToken", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id_type_token");

                    b.Property<string>("ChainId")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("id_chain");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("tx_name");

                    b.Property<string>("Type")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ds_type_token");

                    b.HasKey("Id", "ChainId");

                    b.ToTable("tb_type_token");
                });

            modelBuilder.Entity("BackendToyo.Models.PartPlayer", b =>
                {
                    b.HasOne("BackendToyo.Models.Parts", null)
                        .WithMany()
                        .HasForeignKey("PartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendToyo.Models.Stat", null)
                        .WithMany()
                        .HasForeignKey("StatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackendToyo.Models.Token", b =>
                {
                    b.HasOne("BackendToyo.Models.TypeToken", null)
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackendToyo.Models.ToyoPlayer", b =>
                {
                    b.HasOne("BackendToyo.Models.Toyo", null)
                        .WithMany()
                        .HasForeignKey("ToyoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackendToyo.Models.TxTokenPlayer", b =>
                {
                    b.HasOne("BackendToyo.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendToyo.Models.Token", null)
                        .WithMany()
                        .HasForeignKey("TokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
