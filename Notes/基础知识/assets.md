# 资源 assets

许多应用程序需要包含位图、样式和资源字典等资产。资源字典包含可以在XAML中声明的图形基础元素。样式也可以用XAML编写，但位图资产是二进制文件，例如PNG和JPEG格式的图像。

## 添加资源

可以通过在项目文件中使用 `<AvaloniaResource>` 元素来将资源添加到应用程序中。

例如，Avalonia .NET Core MVVM应用程序解决方案模板会创建一个名为Assets的文件夹（包含avalonia-logo.ico文件），并在项目文件中添加一个元素来包含其中的任何文件，如下所示：

```xml
<ItemGroup>
  <AvaloniaResource Include="Assets\**"/>
</ItemGroup>
```

可以通过在该项组中添加额外的 `<AvaloniaResource>` 元素来添加所需的任何文件。

## 使用资源

在 axaml 文件中，通过 `Source` 属性引用资源。例如：

```xml
<Image Source="icon.png"/>
<Image Source="images/icon.png"/>
<Image Source="../icon.png"/>
<Image Source="/Assets/icon.png"/>
```

## Library 中的资源

如果资产包含在与XAML文件不同的程序集中，则可以使用 `avares:URI`方案。例如，如果资产包含在名为 MyAssembly.dll 的程序集中的 Assets 文件夹中，则可以使用：

```xml
<Image Source="avares://MyAssembly/Assets/icon.png"/>
```

## 类型转换

Avalonia UI内置了用于加载位图、图标和字体资产的转换器。因此，URI可以自动转换为以下任意一种类型：

- 图像 - Image 类型
- 位图 - Bitmap 类型
- 窗口图标 - WindowIcon 类型
- 字体 - FontFamily 类型

## 代码中使用资源

可以编写代码来使用AssetLoader静态类加载资产。例如：

```csharp
var bitmap = new Bitmap(AssetLoader.Open(new Uri(uri)));
```

上面代码中的uri变量可以包含任何带有avares:方案的有效URI（如上所述）。

Avalonia UI 不支持 `file://`，`http://` 或 `https://` 等方案。如果要从磁盘或Web加载文件，必须自己实现该功能或使用社区提供的实现。