using CommunityToolkit.Mvvm.ComponentModel;

namespace NativeAot;

public partial class MainWindowViewModel : ViewModelBase.ViewModelBase
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia! It's running on native code right now!";
}