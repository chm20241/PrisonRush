using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Infrastructure;
//using Spg.GammaShop.Repository;
using Spg.GammaShop.Repository2;
using Spg.GammaShop.RepositoryTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.RepositoryTest
{
    public class RepositorxBaseTest
    {

        [Fact()]
        public void Product_Create_Success_Test()
        {
            using (GammaShopContext db = new GammaShopContext(DatabaseUtilities.GetDbOptions()))
            {

                // Arrange
                DatabaseUtilities.InitializeDatabase(db);


                Product newProduct = new Product(
                    Guid.NewGuid(),
                    "Test Product3",
                    19.99m,
                    null,
                    "This is a test product3.",
                    "test-image.jpg",
                    Rating.SehrGut,
                    100,
                    10,
                    DateTime.Now.AddDays(-15));


                // Act
                new RepositoryBase<Product>(db).Create(newProduct);

                // Assert
                Assert.Equal(3, db.Products.Count());
            }
        }
    }
  
    
}
