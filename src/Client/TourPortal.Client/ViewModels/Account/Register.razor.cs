namespace TourPortal.Client.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Infrastructure.Global.Types;
    using Infrastructure.Shared.Models.Authentication;
    using Microsoft.AspNetCore.Components;

    public partial class Register
    {
		[CascadingParameter] BlazoredModalInstance ModalForm { get; set; }

        private RegisterModel register = new RegisterModel();
        private List<UserRoleModel> UserTypes = new List<UserRoleModel>();
        private SendMessage Message = new SendMessage();
        private string Error = "";

        protected override async Task OnInitializedAsync()
        {
            var request = await ApiService.GetUserRoles();

            if (request.IsOk)
            {
                UserTypes = request
                    .ResponseData
                    .UserRoles
                    .Where(r => r.RoleName != Security.Role.Administrator && r.RoleName != Security.Role.Employe)
                    .Select(x => new UserRoleModel
                    {
                        RoleId = x.RoleId,
                        RoleName = x.RoleName.Contains("User") ?
                            x.RoleName.Replace("User", "Юзър") :
                            x.RoleName.Contains("Owner") ?
                                x.RoleName.Replace("Owner", "Собственик на хотел") : null
                    })
                    .ToList();
            }
        }

        private async Task OnSubmit()
        {
            var result = await ApiService.Register(register);

            if (result.IsOk)
            {
                await GoToLoginForm();
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = string.Join(Environment.NewLine, result.Errors);
            }
        }

        private async Task OpenLoginForm()
        {
            await GoToLoginForm();
        }

        private async Task GoToLoginForm()
        {
            Message.Message = "07b613fa8fbf";
            await ModalForm.CloseAsync(ModalResult.Ok(Message.Message));
        }

        internal class SendMessage
        {
            public string Message { get; set; }
        }
	}
}