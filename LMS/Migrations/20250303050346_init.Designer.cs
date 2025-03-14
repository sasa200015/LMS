﻿// <auto-generated />
using System;
using LMS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMS.Migrations
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20250303050346_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LMS.Model.categories", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LMS.Model.courses", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("course_des")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("course_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("course_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dep_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("level_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("level_id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LMS.Model.enrollments", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("course_id")
                        .HasColumnType("int");

                    b.Property<int>("student_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("course_id");

                    b.HasIndex("student_id");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("LMS.Model.lecture_materials", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("lecture_id")
                        .HasColumnType("int");

                    b.Property<string>("material_pdf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("material_text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("material_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("material_video")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("lecture_id")
                        .IsUnique();

                    b.ToTable("LectureMaterials");
                });

            modelBuilder.Entity("LMS.Model.lectures", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("course_id")
                        .HasColumnType("int");

                    b.Property<string>("lecture_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lecture_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("course_id");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("LMS.Model.students", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LMS.Model.courses", b =>
                {
                    b.HasOne("LMS.Model.categories", "categories")
                        .WithMany()
                        .HasForeignKey("level_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categories");
                });

            modelBuilder.Entity("LMS.Model.enrollments", b =>
                {
                    b.HasOne("LMS.Model.courses", "course")
                        .WithMany("students")
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.Model.students", "student")
                        .WithMany("courses")
                        .HasForeignKey("student_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("student");
                });

            modelBuilder.Entity("LMS.Model.lecture_materials", b =>
                {
                    b.HasOne("LMS.Model.lectures", "lectures")
                        .WithOne("material")
                        .HasForeignKey("LMS.Model.lecture_materials", "lecture_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lectures");
                });

            modelBuilder.Entity("LMS.Model.lectures", b =>
                {
                    b.HasOne("LMS.Model.courses", "courses")
                        .WithMany("lectures")
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("courses");
                });

            modelBuilder.Entity("LMS.Model.courses", b =>
                {
                    b.Navigation("lectures");

                    b.Navigation("students");
                });

            modelBuilder.Entity("LMS.Model.lectures", b =>
                {
                    b.Navigation("material");
                });

            modelBuilder.Entity("LMS.Model.students", b =>
                {
                    b.Navigation("courses");
                });
#pragma warning restore 612, 618
        }
    }
}
