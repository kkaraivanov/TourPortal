namespace TourPortal.Infrastructure.Shared.Models
{
    public class DeletableUserModel
    {
        public string DeletableId  { get; set; }

        public UserSettingModel DeletedUser { get; set; }
    }
}