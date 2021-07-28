namespace TourPortal.Client.Services
{
    using System;
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

    class LogedUserService : ILogedUserService
    {
        public LogedUserModel User { get; private set; }

        public HotelInfoModel Hotel { get; private set; }

        public bool IsLoggedIn => User != null;

        public bool IsHotelExist => Hotel != null;

        public void LogedInUser(LogedUserModel user)
        {
            User = user;
            OnUserLoged?.Invoke();
        }

        public void AddHotel(HotelInfoModel hotel)
        {
            Hotel = hotel;
            OnUserLoged?.Invoke();
        }

        public event Action OnUserLoged;
    }
}