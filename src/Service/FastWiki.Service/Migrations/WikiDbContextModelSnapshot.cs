﻿// <auto-generated />
using System;
using FastWiki.Service.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FastWiki.Service.Migrations
{
    [DbContext(typeof(WikiDbContext))]
    partial class WikiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FastWiki.Service.Domain.ChatApplications.Aggregates.ChatApplication", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ChatModel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("Creator")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxResponseToken")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("Modifier")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Opener")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Parameter")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Prompt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReferenceUpperLimit")
                        .HasColumnType("integer");

                    b.Property<double>("Relevancy")
                        .HasColumnType("double precision");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<string>("Template")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WikiIds")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("wiki-chat-application", (string)null);
                });

            modelBuilder.Entity("FastWiki.Service.Domain.ChatApplications.Aggregates.ChatDialog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ChatId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("wiki-chat-dialog", (string)null);
                });

            modelBuilder.Entity("FastWiki.Service.Domain.ChatApplications.Aggregates.ChatDialogHistory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ChatDialogId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(-1)
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uuid");

                    b.Property<bool>("Current")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uuid");

                    b.Property<int>("TokenConsumption")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChatDialogId");

                    b.HasIndex("Creator");

                    b.ToTable("wiki-chat-dialog-history", (string)null);
                });

            modelBuilder.Entity("FastWiki.Service.Domain.ChatApplications.Aggregates.ChatShare", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("integer");

                    b.Property<long>("AvailableToken")
                        .HasColumnType("bigint");

                    b.Property<string>("ChatApplicationId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChatApplicationId");

                    b.ToTable("wiki-chat-share", (string)null);
                });

            modelBuilder.Entity("FastWiki.Service.Domain.Storage.Aggregates.FileStorage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("Creator")
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCompression")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("Modifier")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("wiki-file-storages", (string)null);
                });

            modelBuilder.Entity("FastWiki.Service.Domain.Users.Aggregates.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("Creator")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDisable")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("Modifier")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("wiki-users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("62769e54-0b0a-4a28-b3a5-b642c52ce8e5"),
                            Account = "admin",
                            Avatar = "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                            CreationTime = new DateTime(2024, 2, 29, 17, 59, 24, 403, DateTimeKind.Utc).AddTicks(1510),
                            Email = "239573049@qq.com",
                            IsDeleted = false,
                            IsDisable = false,
                            ModificationTime = new DateTime(2024, 2, 29, 17, 59, 24, 403, DateTimeKind.Utc).AddTicks(1512),
                            Name = "admin",
                            Password = "a7eeba6911945963ce0eafa6352a7a89",
                            Phone = "13049809673",
                            Salt = "51111786de5e4809b4f72c9670f2c294"
                        });
                });

            modelBuilder.Entity("FastWiki.Service.Domain.Wikis.Aggregates.Wiki", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("Creator")
                        .HasColumnType("uuid");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("Modifier")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("wiki-wikis", (string)null);
                });

            modelBuilder.Entity("FastWiki.Service.Domain.Wikis.Aggregates.WikiDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<int>("DataCount")
                        .HasColumnType("integer");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("Modifier")
                        .HasColumnType("bigint");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<long>("WikiId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("wiki-wiki-details", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
