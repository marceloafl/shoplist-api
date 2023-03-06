using ShoplistAPI.Data;
using ShoplistAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoplistAPIxUnitTests
{
    public class DBUnitTestsMockInitializer
    {
        public DBUnitTestsMockInitializer()
        {}

        public void Seed(ShoplistContext context)
        {
            context.Shoplists.Add
                (new Shoplist { Id = 100, Name = "Supermercado", Description = "Diversos" });

            context.Shoplists.Add
                (new Shoplist { Id = 200, Name = "Feira", Description = "Vegetais" });

            context.Shoplists.Add
                (new Shoplist { Id = 300, Name = "Açougue", Description = "Carnes" });
        }
    }
}
