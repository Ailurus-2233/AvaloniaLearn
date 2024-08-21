# 文件对话框

文件对话框功能可以通过 StorageProvider 服务 API 访问，该服务 API 可在 Window 或 TopLevel 类中获取。

## OpenFilePickerAsync

此方法打开一个文件选择器对话框，允许用户选择文件。FilePickerOpenOptions 定义了传递给操作系统对话框的选项。

```csharp
public class MyView : UserControl
{
    private async void OpenFileButton_Clicked(object sender, RoutedEventArgs args)
    {
        // 从当前控件获取 TopLevel。或者，可以使用 Window 引用。
        var topLevel = TopLevel.GetTopLevel(this);

        // 启动异步操作以打开对话框。
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Text File",
            AllowMultiple = false
        });

        if (files.Count >= 1)
        {
            // 打开第一个文件的读取流。
            await using var stream = await files[0].OpenReadAsync();
            using var streamReader = new StreamReader(stream);
            // 将文件的所有内容作为文本读取。
            var fileContent = await streamReader.ReadToEndAsync();
        }
    }
}
```

## SaveFilePickerAsync

此方法打开一个文件保存对话框，允许用户保存文件。FilePickerSaveOptions 定义了传递给操作系统对话框的选项。

```csharp
public class MyView : UserControl
{
    private async void SaveFileButton_Clicked(object sender, RoutedEventArgs args)
    {
        // 从当前控件获取 TopLevel。或者，可以使用 Window 引用。
        var topLevel = TopLevel.GetTopLevel(this);

        // 启动异步操作以打开对话框。
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Text File"
        });

        if (file is not null)
        {
            // 打开文件的写入流。
            await using var stream = await file.OpenWriteAsync();
            using var streamWriter = new StreamWriter(stream);
            // 将一些内容写入文件。
            await streamWriter.WriteLineAsync("Hello World!");
        }
    }
}
```