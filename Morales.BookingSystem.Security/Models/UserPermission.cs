namespace Morales.BookingSystem.Security.Models
{
    public class UserPermission
    {
        public int UserID { get; set; }
        public LoginUser User { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}