# 样式

Avalonia为控件提供了两种主要的样式机制：

1. 样式（Styles）类似于CSS样式，通常用于根据控件的内容或在应用程序中的用途对控件进行样式化；例如，创建用于标题文本块的样式。
2. 控件主题（ControlTheme）类似于WPF/UWP样式，通常用于对控件应用主题。

## 样式

在 Avalonia 中，Style 更类似于 CSS 样式，而不是 WPF/UWP 样式。在 Avalonia 中，与 WPF/UWP 中的样式相当的是ControlTheme。

> 实质上，样式机制有两个步骤：选择和替换。样式的 XAML 可以定义如何进行这两个步骤，但通常你会在控件元素上定义 'class' 标签来帮助选择步骤。

在选择步骤中，样式系统从控件开始沿着逻辑树向上搜索。这意味着在应用程序的最高级别（例如 App.axaml 文件）定义的样式可以在应用程序的任何地方使用，但仍然可以在控件更近的地方（例如在窗口或用户控件中）进行覆盖。

当选择步骤找到匹配项时，匹配的控件的属性将根据样式中的设置器进行更改。

样式的 XAML 有两个部分：选择器属性和一个或多个设置器元素。选择器的值包含使用 Avalonia UI 样式选择器语法 的字符串。每个设置器元素通过名称标识将被更改的属性和将被替换的新值。模式如下：

```xml
<Style Selector="selector syntax">
     <Setter Property="property name" Value="new value"/>
     ...
</Style>
```

Avalonia UI 样式选择器语法 类似于 CSS（层叠样式表）中使用的语法。

```xml
<Window ... >
    <Window.Styles>
        <Style Selector="TextBlock.h1">
            <Setter Property="FontSize" Value="24"/>
            <Setter Property="FontWeight" Value="Bold"/>
        </Style>
    </Window.Styles>
    <StackPanel Margin="20">
        <TextBlock Classes="h1">Heading 1</TextBlock>
    </StackPanel>
</Window>
```

一般来说，Styles 会置于控件的 Styles 属性中，如果写在 Application 中，那么会对整个应用程序生效。

### 选择器

样式选择器定义样式将作用于哪些控件。选择器使用多种格式，其中最简单的一个如下：

```xml
<Style Selector="TargetControlClass.styleClassName">
```

这个选择器将匹配具有样式键TargetControlClass且带有样式类styleClassName的所有控件。

### 设置器

设置器描述了当选择器与控件匹配时会发生什么。它们是以以下格式编写的简单的属性/值对：

```xml
<Setter Property="FontSize" Value="24"/>
<Setter Property="Padding" Value="4 2 0 4"/>
```

当样式与控件匹配时，样式中的所有设置器都将应用于控件。

### 嵌套样式

样式可以嵌套在其他样式中。要嵌套样式，只需将子样式作为父 `<Style> `元素的子元素包含，并在子选择器的开头加上 嵌套选择器 `^`:

```xml
<Style Selector="TextBlock.h1">
    <Setter Property="FontSize" Value="24"/>
    <Setter Property="FontWeight" Value="Bold"/>
    
    <Style Selector="^:pointerover">
        <Setter Property="Foreground" Value="Red"/>
    </Style>
</Style>
```

当发生这种情况时，父样式的选择器将自动应用于子样式。在上面的示例中，嵌套样式的选择器将是 `TextBlock.h1:pointerover`，这意味着当指针悬停在控件上时，它将显示为红色前景色。

### 样式键

样式选择器匹配的对象的类型不是由控件的具体类型决定的，而是通过检查其 StyleKey 属性来确定的。

默认情况下，StyleKey 属性返回当前实例的类型。然而，如果你希望你的控件（继承自 Button）被样式化为一个按钮，你可以在你的类中重写 StyleKeyOverride 属性，并让它返回 typeof(Button)。

```csharp
public class MyButton : Button
{
    // MyButton 将会被作为标准的 Button 控件样式化。
    protected override Type StyleKeyOverride => typeof(Button);
}
```

### 样式和资源

样式通常与资源一起使用以帮助维护一致的表现。资源可以帮助定义应用程序中的标准颜色和图标，或者在从单独文件中包含时可以跨多个应用程序中使用。

## 样式类

可以为 Avalonia UI 控件分配一个或多个样式类，并使用它们来指导样式选择。样式类通过在控件元素中使用 Classes 属性进行分配。如果你想分配多个类，则使用空格分隔它们。

```xml
<Button Classes="h1 blue"/>
```

### 伪类 Pseudo Classes

与 CSS 类似，控件可以拥有伪类，这些类是在控件本身而不是用户定义的。伪类在选择器中的名称始终以冒号开头。

例如，`:pointerover` 伪类表示指针输入当前悬停在控件上（在控件的边界内）。（这个伪类类似于 CSS 中的 `:hover`。）

这是一个使用 `:pointerover` 伪类选择器的示例：

```xml
<StackPanel>
    <StackPanel.Styles>
        <Style Selector="Border:pointerover">
            <Setter Property="Background" Value="Red"/>
        </Style>
    </StackPanel.Styles>
    <Border>
        <TextBlock>I will have red background when hovered.</TextBlock>
    </Border>
</StackPanel>
```

```xml
<StackPanel>
    <StackPanel.Styles>
        <Style Selector="Button:pressed /template/ ContentPresenter">
            <Setter Property="TextBlock.Foreground" Value="Red"/>
        </Style>
    </StackPanel.Styles>
    <Button>I will have red text when pressed.</Button>
</StackPanel>
```

其他伪类包括 `:focus`、`:disabled`、`:pressed`（用于按钮）和 `:checked`（用于复选框）。

### 条件类 Conditional Classes

如果你需要使用绑定条件添加或删除类，则可以使用以下特殊语法：

```xml
<Button Classes.accent="{Binding IsSpecial}" />
```

### 代码中使用样式类

```csharp
control.Classes.Add("blue");
control.Classes.Remove("red");
```

## 控件主题

控件主题是在样式的基础上构建的，用于为控件创建可切换的主题。尽管控件主题与 WPF/UWP 中的 Style 类似，但它们的机制略有不同。

在 Avalonia 11 之前，控件主题是使用标准样式创建的。然而，这种方法存在一个根本性的问题：一旦样式被应用到控件上，就没有办法移除它。因此，如果你想为特定的控件实例或用户界面（UI）部分更改主题，唯一的选择是将第二个主题应用到控件上，并希望它能覆盖原始主题中设置的所有属性。

这个问题的解决方案在 Avalonia 11 中引入，形式为 控件主题。

控件主题本质上是样式，但有一些重要的区别：

1. 控件主题没有选择器：它们有一个 TargetType 属性，用于描述它们要针对的控件。
2. 控件主题存储在 ResourceDictionary 中，而不是 Styles 集合中。
3. 控件主题通过设置 Theme 属性来分配给控件，通常使用 {StaticResource} 标记扩展。

> 控件主题通常应用于模板化（无外观）的控件，但实际上它们可以应用于任何控件。然而，对于非模板化的控件，通常更方便使用标准样式。

### 示例

App.axaml

```xml
<Application ...>
    <Application.Styles>
        <FluentTheme />
    </Application.Styles>

    <Application.Resources>
        <ControlTheme x:Key="EllipseButton" TargetType="Button">
            <Setter Property="Background" Value="Blue"/>
            <Setter Property="Foreground" Value="Yellow"/>
            <Setter Property="Padding" Value="8"/>
            <Setter Property="Template">
                <ControlTemplate>
                    <Panel>
                        <Ellipse Fill="{TemplateBinding Background}"
                                 HorizontalAlignment="Stretch"
                                 VerticalAlignment="Stretch"/>
                        <ContentPresenter x:Name="PART_ContentPresenter"
                                          Content="{TemplateBinding Content}"
                                          Margin="{TemplateBinding Padding}"/>
                    </Panel>
                </ControlTemplate>
            </Setter>
        </ControlTheme>
    </Application.Resources>
</Application>
```

MainWindow.axaml

```xml
<Window ...>
    <Button Theme="{StaticResource EllipseButton}"
            HorizontalAlignment="Center"
            VerticalAlignment="Center">
        Hello World!
    </Button>
</Window>
```

#### 添加选择器

使用嵌套样式，我们可以使按钮在指针悬停在其上时改变颜色：

```xml
<ControlTheme ...>
    <Style Selector="^:pointerover">
        <Setter Property="Background" Value="Red"/>
        <Setter Property="Foreground" Value="White"/>
    </Style>
</ControlTheme>
```

### 控件主题查找

控件主题有两种查找方式：

1. 如果控件的 Theme 属性被设置，则使用该控件主题
2. Avalonia 会从逻辑树向上搜索控件，查找一个 x:Key 与控件的样式键匹配的 ControlTheme 资源

> 如果你在使用 Avalonia 时发现控件无法找到主题，请确保它返回了与控件主题的 x:Key 和 TargetType 匹配的样式键。

实际上，这意味着有两种选择来定义控件主题：

- 如果希望控件主题应用于控件的所有实例，请将 {x:Type} 用作资源键。例如：`<ControlTheme x:Key="{x:Type Button}" TargetType="Button">`
- 如果希望控件主题应用于选定的控件实例，请使用任何其他内容作为资源键，并使用 {StaticResource} 查找此资源。通常，此键将是一个 string。

### TargetType

ControlTheme.TargetType 属性指定适用 Setter 属性的类型。如果没有指定 TargetType，则必须使用类名限定 Setter 对象中的属性，使用 Property="ClassName.Property" 的语法。例如，不要设置 Property="FontSize"，而应该设置 Property 为 TextBlock.FontSize 或 Control.FontSize。

可以在以下位置查看 Avalonia 内置控件的控件主题：
- [Simple Theme](https://github.com/AvaloniaUI/Avalonia/tree/master/src/Avalonia.Themes.Simple/Controls)
- [Fluent Theme](https://github.com/AvaloniaUI/Avalonia/tree/master/src/Avalonia.Themes.Fluent/Controls)
