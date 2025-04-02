using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FluentDataGridIssueDemo.ViewModels
{
    public abstract partial class ViewModelBase : ObservableObject, IViewModelBase
    {
        public bool IsLoading { get; protected set; } = true;

        public virtual async Task OnInitializedAsync()
        {
            await LoadedAsync().ConfigureAwait(true);
            IsLoading = false;
        }

        protected virtual void NotifyStateChanged()
            => OnPropertyChanged((string?)null);

        [RelayCommand]
        public virtual async Task LoadedAsync()
            => await Task.CompletedTask.ConfigureAwait(true);
    }
}
