# Avalonia xaml

在WPF中使用的 XAML 文件的扩展名是 .xaml，但由于与 Visual Studio 的技术问题整合，Avalonia UI 使用了自己的 .axaml 扩展名意为 Avalonia XAML。

一个常规的 Avalonia XAML 文件如下：

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication1.MainWindow">
</Window>
```

和 xaml 文件一样，首先定义了一个 `Window` 窗体类型，`xmlns` 属性指定了 Avalonia 的命名空间，`x:Class` 属性指定了窗口的类名。值得注意的是

1. `xmlns="https://github.com/avaloniaui` 这是 Avalonia UI 本身的XAML命名空间声明。这是必需的，否则文件将无法被识别为Avalonia XAML文档
2. `xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml` 这是XAML语言命名空间的声明
3. `x:Class="AvaloniaApplication1.MainWindow"` 这是窗口的类名，这个类名是在代码中定义的窗口类名。这个类在代码后台文件中定义，通常由 C# 编写。

## Avalonia 控件

与 WPF 类似，Avalonia 也有一套控件库，可以通过添加表示 Avalonia UI 控件之一的 XML 元素来构建应用程序的用户界面。

例如：

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication1.MainWindow">
    <Button>Click me</Button>
</Window>
```

### 控件属性

Avalonia 控件的属性可以通过 XML 元素的属性来设置。例如：

```xml
<Button Content="Click me" Width="100" Height="50"/>
```

### 控件内容

Avalonia 控件的内容可以通过 XML 元素的内容来设置，也可以通过 `Content` 属性来设置。例如：

```xml
<Button>Click me</Button>
<Button Content="Click me" />
```

### 数据绑定

Avalonia 支持数据绑定，可以通过 `{Binding}` 标记扩展将控件属性链接到 `DataContent` 中的对象。例如：

```xml
<Button Content="{Binding Greeting}"/>
```

## 后端代码文件

一个 Avalonia XAML 文件通常会有一个对应的后端代码文件，这个文件通常是一个 C# 文件，用于处理窗口的事件和逻辑。通常是以 `.axaml.cs` 为后缀的文件。

## xml 命名空间

与任何 XML 格式一样，在 Avalonia XAML 文件中，可以声明命名空间。这允许 XML 处理器找到文件中使用的元素的定义。

可以使用xmlns属性添加命名空间。命名空间声明的格式如下：

```xml
xmlns:alias="definition"
```

通常在根元素中定义要使用的所有命名空间是标准做法。只能在文件中定义一个命名空间而不使用别名部分的属性名。别名必须在文件内始终保持唯一。命名空间声明的定义部分可以是URL或代码定义。这两者都用于定位文件中元素的定义。

### clr-namespace 前缀

有一个clr-namespace:前缀，可以用于引用当前程序集中的代码和引用程序集中的代码。

```xml
<Window ...
    xmlns:myAlias1="clr-namespace:MyNamespace.AppNameSpace.UI" 
... >
```

如果代码在另一个被引用的程序集中（例如一个库中），必须扩展说明以包含被引用程序集的名称：

```xml
<Window ...
    xmlns:myAlias1="clr-namespace:MyNamespace.AppNameSpace.UI;assembly=MyAssemblyName"
... >
```

### using 前缀

有一个using:前缀，可以用于引用当前程序集中的代码的代码。

```xml
<Window ...
    xmlns:myAlias1="using:MyNamespace.AppNameSpace.UI"
... >
```
