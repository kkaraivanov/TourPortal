namespace TourPortal.Client.Services
{
    using Data;

    public interface ILogedUserService
    {
        void LogedInUser(LogedUserModel user);

        void AddHotel(HotelInfoModel hotel);

        LogedUserModel User { get; }

        HotelInfoModel Hotel { get; }

        bool IsLoggedIn { get; }

        bool IsHotelExist { get; }
    }
}