namespace VehicleRepairs.Shared.Common
{
    public static class CommonConstants
    {
        public class Config
        {
            public const int DEFAULT_SKIP = 1;
            public const int DEFAULT_TAKE = 20;
        }

        public class Roles
        {
            public const string SUPER_ADMIN = "Super Admin";
            public const string ADMIN = "Admin";
            public const string STATION = "Station";
            public const string USER = "User";
        }

        public class Messages
        {
            public const string FILE_REQUIRED = "File is required";
        }

        public class Vehicles
        {
            public const string MOTORBIKE = "Motorbike";

            public const string CAR = "Car";
        }

        public class OrderStatus
        {
            public const string WAITING = "Đang chờ";

            public const string ACCEPTED = "Đã chấp nhận";

            public const string REJECTED = "Đã bị từ chối";

            public const string CANCLED = "Đã huỷ";

            public const string DONE = "Đã hoàn thành";
        }

        public class NotificationTypes
        {
            public const string ORDER_TRACKING = "ORDER_TRACKING";

            public const string NOTIFY = "NOTIFY";
        }

        public class ChartTypes
        {
            public const string RANGE = "RANGE";

            public const string MONTH = "MONTH";

            public const string YEAR = "YEAR";
        }
    }
}
