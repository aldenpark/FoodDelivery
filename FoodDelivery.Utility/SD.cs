using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Utility
{
    public class SD //Static Details
    {
        public const string ManagerRole = "Manager";
        public const string DriverRole = "Driver";
        public const string KitchenRole = "Kitchen";
        public const string CustomerRole = "Customer";

        public const string ShoppingCart = "ShoppingCart"; // SessionNameId

        public const float SalesTaxPercent = 0.0825f;
        public const float SalesTaxRate = 8.25f;

        public const string PaymentStatusPending = "Payment Pending";
        public const string PaymentStatusApproved = "Payment Approved";
        public const string PaymentStatusRejected = "Payment Rejected";

        public const string StatusSubmitted = "Order Submitted";
        public const string StatusReady = "Order Ready";
        public const string StatusDelivered = "Order Delivered";
        public const string StatusCancelled = "Order Cancelled";
        public const string StatusRefunded = "Order Refunded";

    }
}
