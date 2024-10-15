using KoiDeliveryOrdering.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Service.Utils
{
    public static class DeliveryDateHelper
    {
        public static DateTime AssumpDeliveryDate(int selectedShippingFeeId, 
            List<ShippingFee> shippingFees,
            DateTime currentDate)
        {
            var selectedShippingFee = shippingFees.FirstOrDefault(
                sf => sf.ShippingFeeId == selectedShippingFeeId);
            if (selectedShippingFee == null) return DateTime.MinValue;

            int dayToAdd = 0;
            switch (selectedShippingFee.EstimatedTime)
            {
                case "Same day":
                    dayToAdd = 0;
                    break;
                case "Next day":
                    dayToAdd = 1;
                    break;
                case "1-2 days":
                    dayToAdd = 2;
                    break;
                case "2-3 days":
                    dayToAdd = 3;
                    break;
            }

            return currentDate.AddDays(dayToAdd);
        }
    }
}
