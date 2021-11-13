﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XSquareCalculationsApi.Persistance;

namespace XSquareCalculationsApi.Migrations
{
    [DbContext(typeof(XSquareCalculationContext))]
    partial class XSquareCalculationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("XSquareCalculationsApi.Entities.Authenticate", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.Property<string>("IdToken")
                        .HasColumnType("varchar(32)")
                        .HasColumnName("ID_TOKEN");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime")
                        .HasColumnName("CREATED_TIME");

                    b.Property<DateTime>("ExpiredDateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("EXPIRED_DATETIME");

                    b.HasKey("UserId", "IdToken");

                    b.ToTable("ATHENTICATES");
                });

            modelBuilder.Entity("XSquareCalculationsApi.Entities.MessagesWithTemplate", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MESSAGE_ID");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("Datetime")
                        .HasColumnName("CREATED_TIME");

                    b.Property<string>("DelFlg")
                        .IsRequired()
                        .HasColumnType("char")
                        .HasColumnName("DEL_FLG");

                    b.Property<string>("Message")
                        .HasColumnType("varchar(140)")
                        .HasColumnName("MESSAGE");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int")
                        .HasColumnName("TEMPLATE_ID");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("Datetime")
                        .HasColumnName("UPDATED_TIME");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.HasKey("MessageId");

                    b.ToTable("MESSAGES_WITH_TEMPLATE");
                });

            modelBuilder.Entity("XSquareCalculationsApi.Entities.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("REQUEST_ID");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("Datetime")
                        .HasColumnName("CREATED_TIME");

                    b.Property<string>("DelFlg")
                        .IsRequired()
                        .HasColumnType("char")
                        .HasColumnName("DEL_FLG");

                    b.Property<string>("RequestContent")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("REQUEST_CONTENT");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("Datetime")
                        .HasColumnName("UPDATED_TIME");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.HasKey("RequestId");

                    b.ToTable("REQUESTS");
                });

            modelBuilder.Entity("XSquareCalculationsApi.Entities.Template", b =>
                {
                    b.Property<int>("TemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TEMPLATE_ID");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("Datetime")
                        .HasColumnName("CREATED_TIME");

                    b.Property<string>("DelFlg")
                        .IsRequired()
                        .HasColumnType("char")
                        .HasColumnName("DEL_FLG");

                    b.Property<int>("DownloadCount")
                        .HasColumnType("int")
                        .HasColumnName("DOWNLOAD_COUNT");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int")
                        .HasColumnName("LIKE_COUNT");

                    b.Property<byte[]>("TemplateBlob")
                        .HasColumnType("mediumblob")
                        .HasColumnName("TEMPLATE_BLOB");

                    b.Property<string>("TemplateName")
                        .HasColumnType("varchar(45)")
                        .HasColumnName("TEMPLATE_NAME");

                    b.Property<string>("ThumbNail")
                        .HasColumnType("text")
                        .HasColumnName("THUMBNAIL");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("Datetime")
                        .HasColumnName("UPDATED_TIME");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.HasKey("TemplateId");

                    b.ToTable("TEMPLATES");
                });

            modelBuilder.Entity("XSquareCalculationsApi.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime")
                        .HasColumnName("CREATED_TIME");

                    b.Property<string>("DelFlg")
                        .HasColumnType("char")
                        .HasColumnName("DEL_FLG");

                    b.Property<int>("LikeNumberSum")
                        .HasColumnType("int")
                        .HasColumnName("LIKE_NUMBER_SUM");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("text")
                        .HasColumnName("PASSWORD_SALT");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime")
                        .HasColumnName("UPDATED_TIME");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("USER_NAME");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("USER_PASSWORD");

                    b.HasKey("UserId");

                    b.ToTable("USERS");
                });
#pragma warning restore 612, 618
        }
    }
}
