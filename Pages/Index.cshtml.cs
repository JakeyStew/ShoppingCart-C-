using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ShoppingKart.Entities;
using ShoppingKart.Models;

namespace ShoppingKart.Pages
{
    public class IndexModel : PageModel
    {
        //Never used this logging method, should definitely consider doing so.
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Products> Products;

        public void OnGet()
        {
            //Get the products from the list for the table.
            ProductsModel productModel = new ProductsModel();
            Products = productModel.FindAll();
        }
    }
}
