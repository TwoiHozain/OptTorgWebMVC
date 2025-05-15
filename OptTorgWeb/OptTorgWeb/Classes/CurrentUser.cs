namespace OptTorgWeb.Classes
{
    public class CurrentUser
    {
        public static bool isCustomer {  get; set; }
        public static long customerId {  get; set; }
        public static long EmployeeId {  get; set; }
        public static string? layout { get; set; }
        public static int role { get; set; } = 1;
    }
}
