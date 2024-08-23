using CommunityToolkit.Mvvm.ComponentModel;

namespace SplashScreen;

public partial class MainWindowViewModel: ViewModelBase.ViewModelBase
{
    [ObservableProperty] private string? _text = "Avalonia is a cross-platform UI framework for dotnet, providing a" +
                                                 " flexible styling system and supporting a wide range of platforms" +
                                                 " such as Windows, macOS, Linux, iOS, Android and WebAssembly. ";
}