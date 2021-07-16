namespace TourPortal.Client.Pages.Account
{
    using System.Threading.Tasks;

    public partial class Logout
    {
        protected override async Task OnInitializedAsync()
        {
            await ApiService.Logout();
            StateHasChanged();
            NavigationManager.NavigateTo("/");
        }
    }
}