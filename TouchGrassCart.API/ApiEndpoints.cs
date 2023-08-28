namespace TouchGrassCart.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";
    
    public static class Products
    {
        private const string Base = $"{ApiBase}/products";
        
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    
    public static class Customers
    {
        private const string Base = $"{ApiBase}/customers";
        
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    
    public static class Cart
    {
        private const string Base = $"{ApiBase}/cart";
        
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string AddToCart = Base;
        public const string CreateCart = $"{Base}/create";
        public const string TotalAmount = $"{Base}/total_amount/{{cartId:guid}}";
        public const string RemoveCartItem = $"{Base}/remove_cartItem/{{id:guid}}";
        public const string DeleteCart = $"{Base}/{{id:guid}}";
    }
    
    public static class CartItem
    {
        private const string Base = $"{ApiBase}/cartItem";
        
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    
    
}