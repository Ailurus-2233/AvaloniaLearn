# Code Behind

Avalonia UI 与 WPF 一样，支持 Code Behind，即在 XAML 文件中定义的窗口可以在后台代码文件中定义事件处理程序和逻辑。代码后台文件通常具有 .axaml.cs 文件扩展名，并且通常在 IDE 中显示在 XAML 文件的下一级。

例如，以下是一个简单的 Avalonia XAML 文件：

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication.Views.MainWindow">
  ...
</Window>
```

对应的代码后台文件可能如下所示：

```csharp
using Avalonia.Controls;

namespace AvaloniaApplication.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
```

当首次添加后端文件时，它只有一个构造函数，该构造函数调用 `InitializeComponent()` 方法。调用此方法在运行时加载 AXAML 是必需的。

## 定位控件

例如，如果在 XAML 文件中定义了一个名为 `MyButton` （可以通过 `Name` 属性或者 `x:Name` 定义）的按钮：

```xml
<Button Name="greetingButton">Hello World</Button>
```

现在，可以通过后端代码中自动生成的 `greetingButton` 字段访问该按钮：

```csharp
greetingButton.Content = "Goodbye World";
```

## 设置属性

可以通过后端代码设置控件的属性。

```csharp
greetingButton.Width = 100;
greetingButton.Height = 50;
```

## 处理事件

任何有用的应用程序都需要执行一些操作！当使用代码后台模式时，需要在后端文件中编写事件处理程序。

事件处理程序编写为后端文件中的方法，然后使用事件属性在 XAML 中引用它们。例如，要为按钮点击事件添加处理程序：

```xml
<Button Click="Button_Click">Click me</Button>
```

然后在后端文件中添加一个名为 `Button_Click` 的方法：

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    greetingButton.Content = "Goodbye World";
}
```

请注意，许多 Avalonia 事件处理程序传递了一个名为 RoutedEventArgs 的特殊参数。它包含有关事件的生成和传播方式的信息。
