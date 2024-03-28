﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VolunteerPlatform.Persistence;

#nullable disable

namespace VolunteerPlatform.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240327180536_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CatTag", b =>
                {
                    b.Property<Guid>("CatId")
                        .HasColumnType("uuid")
                        .HasColumnName("cat_id");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid")
                        .HasColumnName("tags_id");

                    b.HasKey("CatId", "TagsId")
                        .HasName("pk_cat_tag");

                    b.HasIndex("TagsId")
                        .HasDatabaseName("ix_cat_tag_tags_id");

                    b.ToTable("cat_tag", (string)null);
                });

            modelBuilder.Entity("VolunteerPlatform.Domain.Entities.Cat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AnimalAttitude")
                        .HasColumnType("text")
                        .HasColumnName("animal_attitude");

                    b.Property<string>("Color")
                        .HasColumnType("text")
                        .HasColumnName("color");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Health")
                        .HasColumnType("text")
                        .HasColumnName("health");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.Property<string>("PeopleAttitude")
                        .HasColumnType("text")
                        .HasColumnName("people_attitude");

                    b.Property<string>("Place")
                        .HasColumnType("text")
                        .HasColumnName("place");

                    b.Property<bool?>("Vaccine")
                        .HasColumnType("boolean")
                        .HasColumnName("vaccine");

                    b.ComplexProperty<Dictionary<string, object>>("Age", "VolunteerPlatform.Domain.Entities.Cat.Age#Age", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Months")
                                .HasColumnType("integer")
                                .HasColumnName("age_months");

                            b1.Property<int>("Years")
                                .HasColumnType("integer")
                                .HasColumnName("age_years");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Gender", "VolunteerPlatform.Domain.Entities.Cat.Gender#Gender", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("gender_value");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "VolunteerPlatform.Domain.Entities.Cat.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("phone_number_number");
                        });

                    b.HasKey("Id")
                        .HasName("pk_cat");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_cat_owner_id");

                    b.ToTable("cat", (string)null);
                });

            modelBuilder.Entity("VolunteerPlatform.Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ProfilePhoto")
                        .HasColumnType("text")
                        .HasColumnName("profile_photo");

                    b.ComplexProperty<Dictionary<string, object>>("Credentials", "VolunteerPlatform.Domain.Entities.Owner.Credentials#Credentials", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Login")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("credentials_login");

                            b1.Property<string>("PasswordHash")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("credentials_password_hash");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "VolunteerPlatform.Domain.Entities.Owner.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("phone_number_number");
                        });

                    b.HasKey("Id")
                        .HasName("pk_owners");

                    b.ToTable("owners", (string)null);
                });

            modelBuilder.Entity("VolunteerPlatform.Domain.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.HasKey("Id")
                        .HasName("pk_tag");

                    b.ToTable("tag", (string)null);
                });

            modelBuilder.Entity("CatTag", b =>
                {
                    b.HasOne("VolunteerPlatform.Domain.Entities.Cat", null)
                        .WithMany()
                        .HasForeignKey("CatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cat_tag_cat_cat_id");

                    b.HasOne("VolunteerPlatform.Domain.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cat_tag_tag_tags_id");
                });

            modelBuilder.Entity("VolunteerPlatform.Domain.Entities.Cat", b =>
                {
                    b.HasOne("VolunteerPlatform.Domain.Entities.Owner", null)
                        .WithMany("Cats")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("fk_cat_owners_owner_id");
                });

            modelBuilder.Entity("VolunteerPlatform.Domain.Entities.Owner", b =>
                {
                    b.Navigation("Cats");
                });
#pragma warning restore 612, 618
        }
    }
}