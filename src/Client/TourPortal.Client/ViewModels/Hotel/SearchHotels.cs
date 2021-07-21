namespace TourPortal.Client.Shared.Hotel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Shared.Models.HotelRequestResponse;
    using Microsoft.AspNetCore.Components;
    using Radzen;

    public partial class SearchHotels : ComponentBase
    {
        private SearchFreeHotelsRequest model = new SearchFreeHotelsRequest();
        private string dateFormat = "MM/dd/yyyy";
        private int personsCount = 4;
        IEnumerable<DateTime> dates = new DateTime[] { DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1) };
        List<DateTime> datesFrom = new List<DateTime>();
        List<DateTime> datesTo = new List<DateTime>();

        [Parameter]
        public List<SearchFreeHotelsResponse> SerchHotels { get; set; }

        [Parameter]
        public EventCallback<List<SearchFreeHotelsResponse>> OnChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            for (int i = 1; i <= 31; i++)
            {
                datesFrom.Add(DateTime.Today.AddDays(-i));
            }
        }

        private async Task Search()
        {
            if (SerchHotels.Any())
            {
                var item = SerchHotels.Last();
                SerchHotels.Remove(item);

                await OnChanged.InvokeAsync(SerchHotels);
            }
        }

        void OnChange(DateTime? value)
        {
            datesTo.AddRange(datesFrom);
            var dayNow = DateTime.Now.Day;
            var checkedDay = value.Value.Day;
            var count = checkedDay - dayNow;

            for (int i = 1; i <= count; i++)
            {
                datesTo.Add(DateTime.Today.AddDays(i));
            }

            StateHasChanged();
        }

        void DateFrom(DateRenderEventArgs args)
        {
            args.Disabled = datesFrom.Contains(args.Date);
        }

        void DateTo(DateRenderEventArgs args)
        {
            args.Disabled = datesTo.Contains(args.Date);
        }

        void DateRenderSpecial(DateRenderEventArgs args)
        {
            if (dates.Contains(args.Date))
            {
                args.Attributes.Add("style", "background-color: #ff6d41; border-color: white;");
            }
        }
    }
}