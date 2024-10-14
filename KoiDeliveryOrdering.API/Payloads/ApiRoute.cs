namespace KoiDeliveryOrdering.API.Payloads;

public static class ApiRoute
{
    private const string Base = "api";

    public static class User
    {
        public const string GetById = Base + "/users/{id:Guid}";
        public const string GetAll = Base + "/users";
        public const string GetByUsername = Base + "/users/{username}";

        // This only use before implementing (Sign in/Sign up)
        public const string GetAllSenderInformationAsync = Base + "/users/sender-informations";
	}

    public static class DeliveryOrder
    {
        public const string GetById = Base + "/delivery-orders/{id}";
        public const string GetAll = Base + "/delivery-orders";
        public const string Insert = Base + "/delivery-orders";
        public const string Update = Base + "/delivery-orders";
        public const string Remove = Base + "/delivery-orders/{id}";
        public const string GetAllPayment = Base + "/payments";
        public const string GetAllShippingFee = Base + "/shipping-fees";
        public const string GetAllOrderStatus = Base + "/delivery-orders/statuses";
        public const string GetAllAppointmentTime = Base + "/delivery-orders/appointment";
        public const string GetAllVoucherByUsername = Base + "/delivery-orders/vouchers/{username}";

        public const string GetProvinceByCodeLocal = Base + "/delivery-orders/provinces/{code}";
        public const string GetDistrictByCodeLocal = Base + "/delivery-orders/districts/{code}";
        public const string GetWardByCodeLocal = Base + "/delivery-orders/ward/{code}";
    }

    public static class VietnameProvincesOnline
    {
        public const string VPOnlineBaseUrl = "https://provinces.open-api.vn/api";
        public const string GetListProvinces = VPOnlineBaseUrl + "/p";
        public const string GetProvinceByCode = VPOnlineBaseUrl + "/p/{code}";
        public const string GetDistrictByCode = VPOnlineBaseUrl + "/d/{code}";
    }

    public static class NominatimMap
    {
        public const string NominatimUrl = "https://nominatim.openstreetmap.org/";
    }

    public static class Animal
    {
        public const string GetAllAnimalType = Base + "/animals/types";
        public const string GetAllHealthStatus = Base + "/animals/health-statuses";
    }

    public static class Document
    {
        public const string GetAll = Base + "/documents";
    }

    public static class Image
    {
        public const string Upload = Base + "/images/upload";
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