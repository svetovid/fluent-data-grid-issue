using System.Diagnostics.CodeAnalysis;
using FluentDataGridIssueDemo.ViewModels;
using Microsoft.AspNetCore.Components;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace FluentDataGridIssueDemo.Mvvm
{
    [ExcludeFromCodeCoverage]
    public abstract class MvvmComponentBase<TViewModel> : ComponentBase where TViewModel : IViewModelBase
    {
        [Inject]
        [NotNull]
        protected TViewModel ViewModel { get; set; }

        protected override void OnInitialized()
        {
            ViewModel.PropertyChanged += (_, _) => InvokeAsync(StateHasChanged);
            base.OnInitialized();
        }

        protected override Task OnInitializedAsync() => ViewModel.OnInitializedAsync();
    }
}
