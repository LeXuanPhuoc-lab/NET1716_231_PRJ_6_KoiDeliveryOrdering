namespace KoiDeliveryOrdering.API.Payloads;

public static class ApiRoute
{
    private const string Base = "api";

    public static class User
    {
        public const string GetById = Base + "/users/{id}";
    }
}