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
        public const string GetAllPayment = Base + "/payments";
        public const string GetAllShippingFee = Base + "/shipping-fees";
    }

    public static class DailyCareSchedule
    {
        public const string GetById = Base + "/daily-care-schedule/{id}";
        public const string GetAll = Base + "/daily-care-schedule";
        public const string Insert = Base + "/daily-care-schedule/insert";
        public const string Update = Base + "/daily-care-schedule/update";
        public const string Remove = Base + "/daily-care-schedule/{id}/remove";
        public const string GetAllCareTask = Base + "/care-task";
        public const string GetAllDeliveryOrderDetail = Base + "/delivery-order-detail";
    }
}