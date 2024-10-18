namespace KoiDeliveryOrdering.API.Payloads;

public static class ApiRoute
{
    private const string Base = "api";
    public const string APIUrl = "http://localhost:7000";

    public static class User
    {
        public const string GetById = Base + "/users/{userId:Guid}";
        public const string GetAll = Base + "/users";
        public const string GetByUsername = Base + "/users/{username}";
        public const string Remove = Base + "/users/{id}/remove";
        public const string Insert = Base + "/users/insert";
        public const string Update = Base + "/users/update";

        

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

        public const string GetAllProvinceLocal = Base + "/delivery-orders/provinces";
        public const string GetAllDistrictLocal = Base + "/delivery-orders/districts";
        public const string GetAllWardLocal = Base + "/delivery-orders/wards";
        public const string GetProvinceByCodeLocal = Base + "/delivery-orders/provinces/q";
        public const string GetDistrictByCodeLocal = Base + "/delivery-orders/districts/q";
        public const string GetWardByCodeLocal = Base + "/delivery-orders/wards/q";
    }

    public static class VietnameProvincesOnline
    {
        public const string VPOnlineBaseUrl = "https://provinces.open-api.vn/api";
        public const string GetListProvinces = VPOnlineBaseUrl + "/p";
        public const string GetProvinceByCode = VPOnlineBaseUrl + "/p/{code}";
        public const string GetDistrictByCode = VPOnlineBaseUrl + "/d/{code}";
    }

    public static class GiaoHangNhanh
    {
        public const string GHNBaseUrl = "https://online-gateway.ghn.vn/shiip/public-api/master-data";
        public const string GetProvince = GHNBaseUrl + "/province";
        public const string GetDistrict = GHNBaseUrl + "/district";
        public const string GetWard = GHNBaseUrl + "/ward";
    }

    public static class NominatimMap
    {
        public const string NominatimUrl = "https://nominatim.openstreetmap.org/";
    }

    public static class Animal
    {
        public const string GetAllAnimalType = Base + "/animals/types";
        public const string GetAllHealthStatus = Base + "/animals/health-statuses";
        public const string GetAll = Base + "/animals";
        public const string GetById = Base + "/animals/{id}";
        public const string Delete = Base + "/animals/{id}";
        public const string Create = Base +"/animals";
        public const string Update = Base + "/animals";
    }

    public static class Document
    {
        public const string GetAll = Base + "/documents";
        public const string GetById = Base + "/documents/{id}";
        public const string CreateDocument = Base + "/documents";
        public const string UpdateDocument = Base + "/documents/{id}";
        public const string DeleteDocument = Base + "/documents/{id}";
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
        public const string GetAllStaff = Base + "/staff";
    }

    public static class CareTask
    {
        public const string GetById = Base + "/care-task/{id}";
        public const string GetAll = Base + "/care-task";
        public const string Insert = Base + "/care-task/insert";
        public const string Update = Base + "/care-task/update";
        public const string Remove = Base + "/care-task/{id}/remove";
    }

    public static class HereMap
    {
        public const string BaseUrl = "https://geocode.search.hereapi.com/v1/geocode?q={address}&apiKey={apiKey}";
    }

    public static class Garage
    {
        public const string GetById = Base + "/garages/{id}";
        public const string GetAll = Base + "/garages";
        public const string Insert = Base + "/garages";
        public const string Update = Base + "/garages";
        public const string Remove = Base + "/garages/{id}";
    }

    public static class Truck
    {
        public const string GetById = Base + "/truck/{id}";
        public const string GetAll = Base + "/truck";
        public const string Insert = Base + "/truck";
        public const string Update = Base + "/truck";
        public const string Remove = Base + "/truck/{id}";
    }
}