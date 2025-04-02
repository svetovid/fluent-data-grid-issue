using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using static FluentDataGridIssueDemo.ViewModels.TimeLanguagePageViewModel;

namespace FluentDataGridIssueDemo.Components.Pages
{
    public partial class RegionalFormatDialog : IDialogContentComponent<IQueryable<CultureDataItem>>
    {
        private string m_NameFilter = string.Empty;
        private string m_SelectedCulture = string.Empty;

        [Parameter]
        public IQueryable<CultureDataItem> Content { get; set; } = Enumerable.Empty<CultureDataItem>().AsQueryable();

        public IQueryable<CultureDataItem> Content2 { get; set; } =
            CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(x => new CultureDataItem(x.Name, x.DisplayName))
                .AsQueryable();

        [CascadingParameter]
        public FluentDialog? Dialog { get; set; }

        private static GridSort<CultureDataItem> CultureSort
            => GridSort<CultureDataItem>.ByDescending(x => x.DisplayName);

        private IQueryable<CultureDataItem>? FilteredItems
            => Content2.Where(x => x.DisplayName.Contains(m_NameFilter, StringComparison.OrdinalIgnoreCase));

        private string GetRowClass(CultureDataItem info)
            => info.Name == m_SelectedCulture ? "selected" : string.Empty;

        private void HandleCountryFilter(ChangeEventArgs args)
        {
            if (args.Value is string value)
            {
                m_NameFilter = value;
            }
        }

        private void HandleClear()
        {
            if (!string.IsNullOrWhiteSpace(m_NameFilter))
            {
                m_NameFilter = string.Empty;
            }
        }

        private void HandleRowFocus(FluentDataGridRow<CultureDataItem> row)
        {
            if (row.Item is null)
            {
                return;
            }

            m_SelectedCulture = row.Item.Name;
        }

        private async Task CancelAsync() => await Dialog!.CloseAsync();

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(m_SelectedCulture))
            {
                return;
            }

            await Dialog!.CloseAsync(m_SelectedCulture);
        }
    }
}
