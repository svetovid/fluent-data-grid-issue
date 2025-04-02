using System.ComponentModel;

namespace FluentDataGridIssueDemo.ViewModels
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
    }
}
