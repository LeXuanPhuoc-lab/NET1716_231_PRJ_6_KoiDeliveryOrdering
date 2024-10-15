
using KoiDeliveryOrdering.MVCWebApp.Models;

namespace KoiDeliveryOrdering.MVCWebApp.Utils;
public class SortingHelper
{
    public static IEnumerable<DeliveryOrderModel> SortDeliveryOrderByColumn(IEnumerable<DeliveryOrderModel> paginatedOrders, string sort)
    {
        string sortOrder = sort.TrimStart('+', '-');
        bool descendingOrder = sort.StartsWith("-");

        var sortMappings = new Dictionary<string, Func<DeliveryOrderModel, object>>
            {
                { "SENDERNAME", p => p.SenderInformation.SenderName },
                { "RECIPIENTNAME", p => p.RecipientName },
                { "TOTALAMOUNT", p => p.TotalAmount },
                { "ORDERSTATUS", p => p.OrderStatus }
            };

        if (sortMappings.TryGetValue(sortOrder.ToUpper(), out var sortExpression))
        {
            return descendingOrder
                ? paginatedOrders.OrderByDescending(sortExpression)
                : paginatedOrders.OrderBy(sortExpression);
        }
        else
        {
            return paginatedOrders;
        }
    }

    // Other sorting here...
}
