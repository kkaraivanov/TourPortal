namespace TourPortal.Infrastructure.Global
{
    using System;

    public static class Global
    {
        public static class Route
        {
            public const string Login = "api/account/login";
            public const string Register = "api/account/register";
            public const string GetRoles = "api/account/getUserRoles";
            public const string GetUserInfo = "api/account/getUserInfo?userEmail=";
            public const string GetUserData = "api/account/getUserData";
            public const string ChangeUserData = "api/account/changeUserData";
            public const string RegisterEmploye = "api/owner/registerEmploye";
            public const string AddNewHotel = "api/owner/addNewHotel";
            public const string ChangeHotel = "api/owner/changeHotel";
            public const string AddNewRoom = "api/owner/addNewRoom";
            public const string GetEmployes = "api/owner/getEmployes";
            public const string GetEmploye = "api/owner/getEmploye?employeId=";
            public const string GetRoomTypes = "api/owner/getRoomTypes";
            public const string GetRooms = "api/applicationUser/getRooms?";
            public const string GetRoom = "api/applicationUser/getRoom?roomId=";
            public const string GetRoomsCount = "api/applicationUser/getRoomsCout?hotelId=";
            public const string GetHotelInfo = "api/applicationUser/getHotelInfo";
            public const string GetHotelCardInfo = "api/applicationUser/getHotelCardInfo?hotelId=";
            public const string GetEmployeInfo = "api/employe/getInfo";
        }

        public static class ProfileImages
        {
            public static string GetImageUrl(byte[] image)
            {
                if (image != null)
                {
                    var base64 = Convert.ToBase64String(image);
                    var uri = string.Format("data:image/jbg;base64,{0}", base64);
                    return uri;
                }

                return "";
            }
        }

        public static class AllertClass
        {
            public const string Primary = "alert-primary";
            public const string Secondary = "alert-secondary";
            public const string Success = "alert-success";
            public const string Danger = "alert-danger";
            public const string Warning = "alert-warning";
        }
    }
}