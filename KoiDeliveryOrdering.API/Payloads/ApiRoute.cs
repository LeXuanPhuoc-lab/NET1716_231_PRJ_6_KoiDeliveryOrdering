namespace KoiDeliveryOrdering.API.Payloads;

public static class ApiRoute
{
    private const string Base = "api";

    public static class User
    {
        public const string GetById = Base + "/users/{id}";
        public const string GetAll = Base + "/users";
    }

    public static class DeliveryOrder
    {
        public const string GetById = Base + "/delivery-orders/{id}";
        public const string GetAll = Base + "/delivery-orders";
        public const string Insert = Base + "/delivery-orders/insert";
        public const string Update = Base + "/delivery-orders/update";
        public const string Remove = Base + "/delivery-orders/{id}/remove";
    }
}