namespace KoiDeliveryOrdering.Common;

public static class Const
{
    public static string APIEndpoint = "http://localhost:7000/api/";
    
    #region Business Logic

    public static decimal PRICE_PER_KILOGAM = 7000;
    #endregion

    #region Error Codes

    public static int ERROR_EXCEPTION_CODE = 4;

    #endregion

    #region Success Codes

    public static int SUCCESS_INSERT_CODE = 1;
    public static string SUCCESS_INSERT_MSG = "Save data success";

    public static int SUCCESS_READ_CODE = 1;
    public static string SUCCESS_READ_MSG = "Get data success";

    public static int SUCCESS_UPDATE_CODE = 1;
    public static string SUCCESS_UPDATE_MSG = "Update data success";

    public static int SUCCESS_REMOVE_CODE = 1;
    public static string SUCCESS_REMOVE_MSG = "REMOVE data success";

    #endregion

    #region Fail Code

    public static int FAIL_INSERT_CODE = -1;
    public static string FAIL_INSERT_MSG = "Save data fail";

    public static int FAIL_READ_CODE = -1;
    public static string FAIL_READ_MSG = "Get data fail";

    public static int FAIL_UPDATE_CODE = -1;
    public static string FAIL_UPDATE_MSG = "Update data fail";

    public static int FAIL_REMOVE_CODE = -1;
    public static string FAIL_REMOVE_MSG = "REMOVE data fail";

    #endregion

    #region Warning Code

    public static int WARNING_NO_DATA_CODE = 4;
    public static string WARNING_NO_DATA_MSG = "No Data Found";

    #endregion
}