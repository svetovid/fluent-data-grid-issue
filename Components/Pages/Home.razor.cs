using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using static FluentDataGridIssueDemo.ViewModels.TimeLanguagePageViewModel;

namespace FluentDataGridIssueDemo.Components.Pages
{
    public sealed partial class Home
    {
        [Inject]
        private IDialogService? DialogService { get; set; }

        [Parameter]
        public string CurrentCultureName { get; set; } = "en-US";

        [Parameter]
        public IQueryable<RegionalFormatDataItem>? RegionalFormatData { get; set; }

        [Parameter]
        public EventCallback<string> OnCultureSaved { get; set; }

        private string CurrentCultureDisplayName => new CultureInfo(CurrentCultureName).DisplayName;

        public async Task ShowAllRegionalFormatsAsync()
        {
            IQueryable<CultureDataItem> data =
                CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new CultureDataItem(x.Name, x.DisplayName)).AsQueryable();

            IDialogReference dialogRef =
                await DialogService!.ShowDialogAsync(typeof(RegionalFormatDialog), data, new DialogParameters { Height="400px" });
            DialogResult result = await dialogRef.Result;
            if (result.Cancelled || result.Data is null)
            {
                return;
            }

            string newCultureName = (string)result.Data;
            if (newCultureName != CurrentCultureName)
            {
                await OnCultureSaved.InvokeAsync(newCultureName);
            }
        }
    }
}
