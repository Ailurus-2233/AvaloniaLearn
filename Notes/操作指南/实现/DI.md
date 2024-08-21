# 如何在 Avalonia 中实现依赖注入

依赖注入(DI) 允许开发人员编写更简洁、模块化和可测试的代码。它通过创建分立的服务来实现这一点，这些服务会根据需要被传递/创建。本指南将逐步向您介绍如何在 Avalonia UI 和 MVVM 模式中使用依赖注入（DI）。

## step 0: 背景与初始代码

假设您有一个包含 MainViewModel、BusinessService 和 Repository 的应用程序。MainViewModel 依赖于 IBusinessService，而 BusinessService 依赖于 IRepository。简单的实现过程如下：

```csharp
public partial class MainViewModel
{
    private readonly IBusinessService _businessService;

    public MainViewModel(IBusinessService businessService)
    {
        _businessService = businessService;
    }
}
```

```csharp
public class BusinessService : IBusinessService
{
    private readonly IRepository _repository;

    public BusinessService(IRepository repository)
    {
        _repository = repository;
    }
}
```

```csharp
public class Repository : IRepository
{
}
```

通常情况下会直接实例化 Repository 并将其传入 BusinessService 然后再传入 MainViewModel，就像这样：

```csharp
var window = new MainWindow
{
    DataContext = new MainViewModel(new BusinessService(new Repository()))
}
```

对于不经常使用且不会改变的简单构造函数，这种方法非常有效。但这种技术的扩展性并不好，因为：

1. 构造函数的依赖关系越多，需要实例化和传入的东西就越多。在本地实例化依赖项（例如通过执行 `new MainViewModel(new MyService())`）会导致与依赖项的特定实例直接刚性耦合。
2. 如果 MainViewModel 自己创建其依赖关系（如在构造函数主体中），它也会直接与依赖关系的创建耦合，从而导致大部分相同的问题。
3. 如果对象在许多地方被实例化，当 MainViewModel 的依赖关系发生变化时（如需要额外的依赖关系或需要依赖关系的不同实现），需要对每个引用都进行更新。

依赖注入通过抽象对象的创建及其依赖关系解决了这些问题。这样就可以使用封装良好的服务，这些服务会自动传递给注册使用它们的任何其他服务。

## step 1: 安装 DI 的 NuGet 包

目前有许多依赖注入（DI）容器的 Nuget 包（DryIoC、Autofac、Pure.DI），这里安装的DI包为 "Microsoft.Extensions.DependencyInjection"，它是一个轻量级、可扩展的依赖注入容器。它为.NET 应用程序（包括基于 Avalonia 的桌面应用程序）添加依赖注入提供了一种易于使用且基于约定的方法。

## step 2: 添加服务集合扩展

```csharp
public static class ServiceCollectionExtensions 
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IRepository, Repository>();
        collection.AddTransient<IBusinessService, BusinessService>();
        collection.AddTransient<MainViewModel>();
    }
}
```

## step 3: 修改 App.xaml.cs

应修改 App.xaml.cs 类以使用 DI 容器。这将能通过依赖注入容器解析先前注册的视图模型。然后就可以将完全实现的视图模型设置为主视图的数据上下文。

```csharp
public class App : Application
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

        // 从 collection 提供的 IServiceCollection 中创建包含服务的 ServiceProvider
        var services = collection.BuildServiceProvider();

        var vm = services.GetRequiredService<MainViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
```
