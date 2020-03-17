namespace ShoppingKart.Entities
{
    public class Products
    {        
        //Model for all the items, holds the data of all items added on the Index page.
        public int ID { get; set; }
        public string ItemPhoto { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        //Not 100% on these, but will do for now.
        public double OriginalItemPrice { get; set; }
        public bool ItemSale { get; set; }
        public int ItemSaleQuantity { get; set; }
        public double ItemSalePrice { get; set; }
    }
}
