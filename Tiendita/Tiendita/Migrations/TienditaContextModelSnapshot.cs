﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tiendita.Models;

namespace Tiendita.Migrations
{
    [DbContext(typeof(TienditaContext))]
    partial class TienditaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Tiendita.Models.Detalle", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<uint?>("DetalleId")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("ProductoId")
                        .HasColumnType("int unsigned");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<uint>("VentaId")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("DetalleId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("VentaId");

                    b.ToTable("Detalles");
                });

            modelBuilder.Entity("Tiendita.Models.Producto", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Costo")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Tamano")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Tiendita.Models.Venta", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<string>("Cliente")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("fecha")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("Tiendita.Models.Detalle", b =>
                {
                    b.HasOne("Tiendita.Models.Detalle", null)
                        .WithMany("Detalles")
                        .HasForeignKey("DetalleId");

                    b.HasOne("Tiendita.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiendita.Models.Venta", "Venta")
                        .WithMany()
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
