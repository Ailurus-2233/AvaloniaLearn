using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using DependencyInjection.Extensions;
using DependencyInjection.Services;
using DependencyInjection.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // 如果使用 CommunityToolkit，则需要用下面一行移除 Avalonia 数据验证。
        // 如果没有这一行，数据验证将会在 Avalonia 和 CommunityToolkit 中重复。
        BindingPlugins.DataValidators.RemoveAt(0);

        // 注册应用程序运行所需的所有服务
        var collection = new ServiceCollection();
        collection.AddCommonServices();
        
        var services = collection.BuildServiceProvider();

        var vm = services.GetService<MainViewModel>();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = vm,
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}