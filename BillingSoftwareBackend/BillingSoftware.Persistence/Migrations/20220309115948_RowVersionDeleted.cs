using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillingSoftware.Persistence.Migrations
{
    public partial class RowVersionDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "OrderConfirmations");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DocumentInformations");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DeliveryNotes");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Products",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Positions",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "OrderConfirmations",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Offers",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Invoices",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DocumentInformations",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DeliveryNotes",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Contacts",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Companies",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Addresses",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
