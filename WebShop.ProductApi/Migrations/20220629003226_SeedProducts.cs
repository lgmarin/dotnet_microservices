using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.ProductApi.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)"+
            " Values('Caderno', 7.55, 'Caderno', 10, 'caderno1.jpg', 1)");
            migrationBuilder.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)"+
            " Values('Penal', 9.55, 'Estojo para quem não manja', 12, 'penal1.jpg', 1)");
            migrationBuilder.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)"+
            " Values('Vina', 17.55, 'Salsicha diferentona', 20, 'linguicao1.jpg', 2)");                        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Products");
        }
    }
}
