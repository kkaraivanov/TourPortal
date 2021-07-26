namespace TourPortal.Infrastructure.Global
{
    public static class Global
    {
        public static class Route
        {
            public const string Login = "api/account/login";
            public const string Register = "api/account/register";
            public const string GetRoles = "api/account/getUserRoles";
            public const string GetUserInfo = "api/account/getUserInfo?userEmail=";
            public const string GetHotelInfo = "api/applicationUser/getHotelInfo";
            public const string AddNewHotel = "api/owner/addNewHotel";
            public const string ChangeHotel = "api/owner/changeHotel";
            public const string GetRooms = "api/owner/getRooms?hotelId=";
            public const string GetRoomTypes = "api/owner/getRoomTypes";
        }
    }
}