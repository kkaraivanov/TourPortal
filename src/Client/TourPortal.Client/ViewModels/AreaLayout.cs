﻿namespace TourPortal.Client.Shared
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;

    public partial class AreaLayout
    {
		private string[] scriptPaths = new string[]
        {
            "css/area-style/js/animate.js",
            "css/area-style/js/bootstrap-select.js",
            "css/area-style/js/owl.carousel.js",
            "css/area-style/js/utils.js",
            "css/area-style/js/perfect-scrollbar.min.js",
            "css/area-style/js/site.js"
        };
        private string[] cssPaths = new string[]
        {
            "css/area-style/css/bootstrap.min.css",
            "css/area-style/style.css",
            "css/area-style/css/responsive.css",
            "css/area-style/css/perfect-scrollbar.css",
            "css/area-style/css/custom.css"
        };
        private bool areaIsReady = false;

        protected override async Task OnInitializedAsync()
        {
            var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/site.js");

            if (!areaIsReady)
            {
                await RemoveElementsFromTemplate(module);
                Thread.Sleep(500);
                await AddStyles(module);
                Thread.Sleep(500);
                await AddScripts(module);
                areaIsReady = true;
            }
        }

        private async Task AddStyles(IJSObjectReference module)
        {
            foreach (var cssPath in cssPaths)
            {
                await module.InvokeVoidAsync("addCss", cssPath);
            }
        }

        private async Task AddScripts(IJSObjectReference module)
        {
            foreach (var scriptPath in scriptPaths)
            {
                await module.InvokeVoidAsync("addScripts", scriptPath);
            }
        }

        private async Task RemoveElementsFromTemplate(IJSObjectReference module)
        {
            await module.InvokeVoidAsync("removeScripts");
            await module.InvokeVoidAsync("removeCss");
        }
	}
}