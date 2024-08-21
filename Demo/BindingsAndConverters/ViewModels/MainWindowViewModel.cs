using CommunityToolkit.Mvvm.ComponentModel;

namespace BindingsAndConverters.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private int _age;
}