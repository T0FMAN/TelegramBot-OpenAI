﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TelegramBot_OpenAI.Data.DB;

#nullable disable

namespace TelegramBot_OpenAI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230917074244_EditedTelegramUsers")]
    partial class EditedTelegramUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TelegramBot_OpenAI.Models.GeneratedImage", b =>
                {
                    b.Property<int>("IdImage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdImage"));

                    b.Property<DateTime>("DateGenerated")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGenerated")
                        .HasColumnType("int");

                    b.Property<string>("Prompt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdImage");

                    b.HasIndex("IdGenerated");

                    b.ToTable("GeneratedImages");
                });

            modelBuilder.Entity("TelegramBot_OpenAI.Models.GeneratedText", b =>
                {
                    b.Property<int>("IdText")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdText"));

                    b.Property<DateTime>("DateGenerated")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGenerated")
                        .HasColumnType("int");

                    b.Property<string>("Prompt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextOutput")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdText");

                    b.HasIndex("IdGenerated");

                    b.ToTable("GeneratedTexts");
                });

            modelBuilder.Entity("TelegramBot_OpenAI.Models.TelegramUser", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<decimal>("AccountBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountGenerations")
                        .HasColumnType("int");

                    b.Property<int?>("CountReferals")
                        .HasColumnType("int");

                    b.Property<int?>("IdInvited")
                        .HasColumnType("int");

                    b.Property<bool>("IsReg")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastActionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RegestrationDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserAction")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.HasIndex("IdInvited");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TelegramBot_OpenAI.Models.GeneratedImage", b =>
                {
                    b.HasOne("TelegramBot_OpenAI.Models.TelegramUser", "User")
                        .WithMany()
                        .HasForeignKey("IdGenerated")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TelegramBot_OpenAI.Models.GeneratedText", b =>
                {
                    b.HasOne("TelegramBot_OpenAI.Models.TelegramUser", "User")
                        .WithMany()
                        .HasForeignKey("IdGenerated")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TelegramBot_OpenAI.Models.TelegramUser", b =>
                {
                    b.HasOne("TelegramBot_OpenAI.Models.TelegramUser", "InvitedUser")
                        .WithMany()
                        .HasForeignKey("IdInvited");

                    b.Navigation("InvitedUser");
                });
#pragma warning restore 612, 618
        }
    }
}
