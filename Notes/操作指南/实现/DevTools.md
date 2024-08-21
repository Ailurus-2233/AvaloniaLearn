# 开发者工具

Avalonia 内置了一个开发工具窗口，可以通过在 Window 构造函数中调用附加的 AttachDevTools() 方法来启用。在程序以 DEBUG 模式编译时，默认模板已启用此功能：

```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}

// 在由 Avalonia.NameGenerator 自动生成的 MainWindow.g.cs中：
partial class MainWindow
{
    // ...
    public void InitializeComponent(bool loadXaml = true, bool attachDevTools = true)
    {
        // ...
#if DEBUG
        if (attachDevTools)
        {
            this.AttachDevTools();
        }
#endif
        // ...
    }
}
```

要打开 DevTools，请按 F12 键，或向 this.AttachDevTools() 方法传递不同的 Gesture 手势。

> 要使用 DevTools，必须添加 Avalonia.Diagnostics nuget 软件包。

## 逻辑树和视觉树

Logical Tree 和 Visual Tree 选项卡显示窗口逻辑树和视觉树中的控件。选择一个控件后，右侧窗格中将显示该控件的属性，可以对其进行编辑。

### 属性

允许快速检查和编辑控件的属性。还可以搜索属性（按名称或使用正则表达式）。

| 列        | 描述   |
|----------|------|
| Property | 属性名称 |
| Value    | 属性值  |
| Type     | 属性类型 |
| Priority | 优先级  |

### 布局

允许检查和编辑常见的布局属性（Margin、Border、Padding），控件的尺寸和尺寸约束也会显示。

### 样式

属性面板显示属性的当前活动值，而样式面板则显示所有值及其来源。

此外，还可以查看可能与该控件匹配的所有样式（通过切换 Show inactive 选项）。

按下 Snapshot 按钮或将鼠标悬停在目标窗口上时按下 Alt+S，即可快照当前样式。快照意味着样式面板不会更新以反映控件的新状态。这在排除与 :pointerover 或 :pressed 选择器有关的问题时很有用。

### 事件

事件选项卡可用于跟踪 事件 的传播。在左侧窗格中选择要跟踪的事件类型，所有此类型的事件将显示在中上方窗格中。选择其中一个事件以查看事件路由。

| 组合键        | 功能                |
|------------|-------------------|
| Alt+S	     | 启用快照样式            |
| Alt+D      | 	禁用快照样式           |
| CTRL+Shift | 	检查指针指向的控件        |
| CTRL+Alt+F | 	切换弹出窗口冻结         |
| F8         | 	对逻辑树或视觉树中的选定项目截图 |