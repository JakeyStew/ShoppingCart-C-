

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingKart.Entities;
using ShoppingKart.Helpers;
using ShoppingKart.Models;
using System.Collections.Generic;

namespace ShoppingKart.Pages
{
    public class BasketModel : PageModel
    {
        public List<Item> Basket { get; set; }
        public double Total { get; set; }
        public void OnGet(int[] quantities)
        {
            //Get session data. Product and Quantity.
            Basket = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "basket");
            if (Basket != null)
            {
                //Loop through the items to determine if they are on offer or not.
                foreach(var item in Basket)
                {
                    //Quantity meets the offer amount?
                    if(item.ItemQuantity == item.Product.ItemSaleQuantity)
                    {
                        //Set the product price to the offers price.
                        item.Product.ItemPrice = item.Product.ItemSalePrice;
                        var TotalPrice = item.Product.ItemPrice * item.ItemQuantity;
                        Total += TotalPrice;
                    }
                    //Quantity greater than the offer amount?
                    else if(item.ItemQuantity > item.Product.ItemSaleQuantity)
                    {
                        //Little trickier here, get the price of the item on offer. 
                        //Then get the original price and add those together giving you a offer price plus any extras at normal price.

                        /****************************************************
                        *               FUTURE IDEA                         *
                        * After the first offer, if they reach another      *
                        * quantity for the item then apply the offer again. *
                        *                                                   *
                        *****************************************************/
                        var SalePrice = item.Product.ItemSalePrice * item.Product.ItemSaleQuantity;
                        var DefaultPrice = item.Product.OriginalItemPrice;
                        var TotalPrice = SalePrice + DefaultPrice * (item.ItemQuantity - item.Product.ItemSaleQuantity);

                        Total += TotalPrice;
                    }
                    //Quantity less than the offer?
                    else if (item.ItemQuantity < item.Product.ItemSaleQuantity)
                    {
                        //Usual pricing applied.
                        item.Product.ItemPrice = item.Product.OriginalItemPrice;
                        Total += item.Product.ItemPrice * item.ItemQuantity;
                    }
                }
            }
        }

        //When "Buy Now" is pressed on Index.
        public IActionResult OnGetBuyNow(int id)
        {
            //Get the product model.
            var productModel = new ProductsModel();
            Basket = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "basket");
            if (Basket == null)
            {
                //Store each Product as an Item in a new list "Basket".
                Basket = new List<Item>
                {
                    new Item
                    {
                        Product = productModel.Find(id),
                        ItemQuantity = 1
                    }
                };
                //Store this new List in the Server Session storage.
                SessionHelper.SetObjectAsJson(HttpContext.Session, "basket", Basket);
            }
            else
            {
                //If the Basket already exists, confirm is the Item is already in the List.
                int index = Exists(Basket, id.ToString());
                if (index == -1)
                {
                    Basket.Add(new Item
                    {
                        Product = productModel.Find(id),
                        ItemQuantity = 1
                    });
                }
                else
                {
                    //If already exist, Increment the amount of that item.
                    Basket[index].ItemQuantity++;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "basket", Basket);
            }
            return RedirectToPage("Basket");
        }

        //When the "X" on the shopping cart is pressed.
        public IActionResult OnGetDelete(int id)
        {
            //Get the data from the session.
            Basket = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "basket");
            //Remove the item from the list.
            int index = Exists(Basket, id.ToString());
            Basket.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "basket", Basket);
            return RedirectToPage("Basket");
        }

        //Update button pressed on the shopping cart page.
        public IActionResult OnPostUpdate(int[] quantities)
        {
            Basket = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "basket");
            //Get the input quantities amount and add any extra to the quantity amount.
            for (var i = 0; i < Basket.Count; i++)
            {
                Basket[i].ItemQuantity = quantities[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "basket", Basket);
            return RedirectToPage("Basket");
        }

        //Checks if an Item already exists in the "Basket" list.
        private int Exists(List<Item> basket, string id)
        {
            for (var i = 0; i < basket.Count; i++)
            {
                if (basket[i].Product.ID.ToString() == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}