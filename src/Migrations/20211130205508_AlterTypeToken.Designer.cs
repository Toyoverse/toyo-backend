﻿// <auto-generated />
using System;
using BackendToyo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackendToyo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211130205508_AlterTypeToken")]
    partial class AlterTypeToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Gwei")
                        .HasColumnType("int");

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

                    b.ToTable("SmartContractToyoTypes");
                });

            modelBuilder.Entity("BackendToyo.Models.Token", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("NFTId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Tokens");
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
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("MetaDataUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("TypeTokens");
                });

            modelBuilder.Entity("BackendToyo.Models.Token", b =>
                {
                    b.HasOne("BackendToyo.Models.TypeToken", null)
                        .WithMany()
                        .HasForeignKey("TypeId")
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
