using ShoppingKart.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingKart.Models
{
    public class ProductsModel
    {
        //Create a list to store all the products
        private List<Products> Products;

        public ProductsModel()
        {
            //Simple list model over using a DB. An improvement for the future?
            Products = new List<Products>() {
                new Products
                {
                    ID = 0,
                    ItemName = "Yeezy Boost",
                    ItemPrice = 5.00,
                    ItemPhoto = "/Products/Yeezy-Boost.jpg",
                    OriginalItemPrice = 5.00,
                    ItemSale = true,
                    ItemSaleQuantity = 3,
                    ItemSalePrice = 4.33
                },
                new Products
                {
                    ID = 1,
                    ItemName = "Addidas Superstar",
                    ItemPrice = 3.00,
                    ItemPhoto = "/Products/Addidas-Superstar.jpg",
                    OriginalItemPrice = 3.00,
                    ItemSale = true,
                    ItemSaleQuantity = 2,
                    ItemSalePrice = 2.25
                },
                new Products
                {
                    ID = 2,
                    ItemName = "Nike Revolution",
                    ItemPrice = 2.00,
                    ItemPhoto = "/Products/Nike-Revolution.jpg",
                    OriginalItemPrice = 2.00,
                    ItemSale = false,
                    ItemSaleQuantity = 0,
                    ItemSalePrice = 0.00
                },
                new Products
                {
                    ID = 3,
                    ItemName = "Nike Blazer",
                    ItemPrice = 1.50,
                    ItemPhoto = "/Products/Nike-Blazer.jpg",
                    OriginalItemPrice = 1.50,
                    ItemSale = false,
                    ItemSaleQuantity = 0,
                    ItemSalePrice = 0.00
                }
            };
        }

        public List<Products> FindAll()
        {
            return Products;
        }

        public Products Find(int id)
        {
            return Products.Where(p => p.ID == id).FirstOrDefault();
        }
    }
}
