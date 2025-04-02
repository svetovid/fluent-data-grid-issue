using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentDataGridIssueDemo.Models;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Globalization;

namespace FluentDataGridIssueDemo.ViewModels
{
    public sealed partial class TimeLanguagePageViewModel(IToastService toastService) : ViewModelBase
    {
        public record RegionalFormatDataItem(string Name, string Value);
        public record CultureDataItem(string Name, string DisplayName);

        [ObservableProperty]
        private RegionSettingModel? m_CurrentRegionSettingModel;

        [ObservableProperty]
        private IQueryable<RegionalFormatDataItem>? m_RegionalFormatData;

        public override async Task OnInitializedAsync()
        {
                RegionSettingModel storedSettingsModel = new() { CurrentCultureName = "en-US" };
                if (storedSettingsModel is not null)
                {
                    CurrentRegionSettingModel = new RegionSettingModel
                    {
                        CurrentCultureName = storedSettingsModel.CurrentCultureName,
                    };

                    UpdateRegionalFormatData(CurrentRegionSettingModel.CurrentCultureName);
                }

            await base.OnInitializedAsync();
        }

        private void UpdateRegionalFormatData(string cultureName)
        {
            Thread.CurrentThread.CurrentCulture.ClearCachedData();
            DateTimeFormatInfo formatInfo = CultureInfo.GetCultureInfo(cultureName).DateTimeFormat;
            var cultureInfo = new CultureInfo(cultureName);

            RegionalFormatData = new[]
            {
                new RegionalFormatDataItem("Calendar", formatInfo.NativeCalendarName),
                new RegionalFormatDataItem("First day of week", formatInfo.GetDayName(formatInfo.FirstDayOfWeek) ),
                new RegionalFormatDataItem("Short date", DateTime.Now.ToString("d", cultureInfo) ),
                new RegionalFormatDataItem("Long date", DateTime.Now.ToString("D", cultureInfo) ),
                new RegionalFormatDataItem("Short time", DateTime.Now.ToString("t", cultureInfo) ),
                new RegionalFormatDataItem("Long time", DateTime.Now.ToString("T", cultureInfo) ),
                new RegionalFormatDataItem("Decimal", (-1234.56).ToString("F", cultureInfo))
            }.AsQueryable();
        }

        [RelayCommand]
        private Task SaveCultureAsync(string newCultureName)
        {
            return Task.CompletedTask;
        }
    }
}
