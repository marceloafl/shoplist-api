using ShoplistAPI.Data;
using ShoplistAPI.Model;

namespace ShoplistAPIxUnitTests
{
    public class DBUnitTestsMockInitializer
    {
        public DBUnitTestsMockInitializer()
        {}

        public void Seed(ShoplistContext context)
        {
            context.Shoplists.Add
                (new Shoplist { Name = "Supermercado", Description = "Diversos" });

            context.Shoplists.Add
                (new Shoplist { Name = "Feira", Description = "Vegetais" });

            context.Shoplists.Add
                (new Shoplist { Name = "Açougue", Description = "Carnes" });

            context.Products.Add
                (new Product
                {
                    Name = "Laranja",
                    Brand = "Marca de laranja",
                    Description = "Descrição da laranja",
                    Number = 2,
                    ShoplistId = 2,
                });

            context.Products.Add
                (new Product
                {
                    Name = "Arroz",
                    Brand = "Marca de arroz",
                    Description = "Descrição do arroz",
                    Number = 1,
                    ShoplistId = 1,
                });

            context.Products.Add
                (new Product
                {
                    Name = "Picanha",
                    Brand = "Marca da picanha",
                    Description = "Descrição da picanha",
                    Number = 3,
                    ShoplistId = 3,
                });

            context.SaveChanges();
        }
    }
}
