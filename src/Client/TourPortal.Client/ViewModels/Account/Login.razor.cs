namespace TourPortal.Client.Pages.Account
{
    using System;
    using System.Threading.Tasks;
    using Blazored.Modal;
    using Infrastructure.Shared.Models.Authentication;
    using Microsoft.AspNetCore.Components;

    public partial class Login
    {
		[CascadingParameter] BlazoredModalInstance ModalForm { get; set; }

        private LoginModel loginModel = new LoginModel();
        private string Error = "";

        private async Task HandleLogin()
        {
            var result = await ApiService.Login(loginModel);

            if (result.IsOk)
            {
                await Close();
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = string.Join(Environment.NewLine, result.Errors);
            }
        }

        private async Task Close()
        {
            await ModalForm.CloseAsync();
        }
	}
}