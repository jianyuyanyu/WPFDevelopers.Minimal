# WidgetDesign.Avalonia 使用文档

## 安装

1. 添加项目引用或 NuGet 包
2. 在项目中引入命名空间：
```xml
xmlns:wd="https://github.com/WPFDevelopersOrg/WidgetDesign.Avalonia"
```

## 主题初始化

在 `App.axaml` 中配置主题资源：

```xml
<Application.Resources>
    <wd:Resources Theme="Dark"/>
</Application.Resources>
```

支持的主题：
- `Light` — 浅色主题
- `Dark` — 深色主题

### 运行时切换主题

```csharp
Application.Current?.SetTheme(_isDark ? ThemeType.Dark : ThemeType.Light);
```

### 动态换色

```csharp
((Resources)Application.Current.Resources).Color = Color.Parse("#FF4D6D");
```

## 控件

### 按钮 (Button)

支持多种主题样式，通过 `Theme` 属性指定：

```xml
<Button Content="Primary" Theme="{StaticResource wd-primary}" />
<Button Content="Default" Theme="{StaticResource wd-default}" />
<Button Content="Normal" Theme="{StaticResource wd-normal}" />
<Button Content="Success" Theme="{StaticResource wd-success}" />
<Button Content="Warning" Theme="{StaticResource wd-warning}" />
<Button Content="Danger" Theme="{StaticResource wd-danger}" />
<Button Content="Filled Success" Theme="{StaticResource wd-success-filled}" />
<Button Content="Filled Warning" Theme="{StaticResource wd-warning-filled}" />
<Button Content="Filled Danger" Theme="{StaticResource wd-danger-filled}" />
```

### 图标 (PathIconEx)

自定义控件，通过 `Kind` 属性选择图标：

```xml
<controls:PathIconEx Kind="Home" Width="24" Height="24"
                     Foreground="{DynamicResource WD.PrimaryBrush}" />
```

支持的图标类型：`Home`, `CheckMark`, `Warning`, `Error`, `Info`, `Star`, `Add`, `Time`, `DatePicker`, `Copy`, `Undo`, `Filter`

也可以直接使用静态几何资源：

```xml
<PathIcon Data="{StaticResource WD.CheckMarkGeometry}"
          Foreground="{DynamicResource WD.SuccessBrush}" Width="24" Height="24" />
```

### TextBox

```xml
<!-- 普通输入框 -->
<TextBox Watermark="Enter text here..." />

<!-- 带清除按钮 -->
<TextBox Watermark="Clear text" Classes="IsClear" />

<!-- 密码框 -->
<TextBox PasswordChar="●" RevealPassword="False" />

<!-- 密码框 + 清除按钮 -->
<TextBox PasswordChar="●" Classes="IsClear" RevealPassword="False" />
```

- 密码框输入内容后自动显示"显示密码"眼睛图标
- 右键支持上下文菜单（剪切/复制/粘贴）

### CheckBox

```xml
<CheckBox Content="已选中" IsChecked="True" />
<CheckBox Content="未选中" />
<CheckBox Content="三态" IsThreeState="True" IsChecked="{x:Null}" />
```

### RadioButton

```xml
<RadioButton Content="Option A" GroupName="demo" IsChecked="True" />
<RadioButton Content="Option B" GroupName="demo" />
```

### ToggleButton / 开关

```xml
<ToggleButton Content="Toggle Off" />
<ToggleButton Content="Toggle On" IsChecked="True" />

<!-- 开关样式 -->
<ToggleButton Classes="wd-switch" IsChecked="True" />
<ToggleButton Classes="wd-switch" />
```

### Tag

```xml
<controls:Tag Content="Default Tag" Close="Tag_Close" />
<controls:Tag Content="Non-closable" IsClose="False" />
```

### ComboBox

```xml
<ComboBox Width="200" SelectedIndex="0">
    <ComboBoxItem Content="Option 1" />
    <ComboBoxItem Content="Option 2" />
    <ComboBoxItem Content="Option 3" />
</ComboBox>
```

### DatePicker / Calendar / CalendarDatePicker

```xml
<DatePicker Width="200" />
<Calendar />
<CalendarDatePicker Width="200" />
```

### TimePicker

```xml
<TimePicker Width="200" />
<TimePicker Width="200" UseSeconds="True" />
<TimePicker Width="200" ClockIdentifier="12HourClock" />
```

### NumericUpDown

```xml
<NumericUpDown Width="200" Value="42" Minimum="0" Maximum="1000" />
```

### DataGrid

```xml
<DataGrid x:DataType="local:DemoItem">
    <DataGrid.Columns>
        <DataGridTextColumn Header="Name" Binding="{Binding Name}" Width="150" />
        <DataGridTextColumn Header="Age" Binding="{Binding Age}" Width="80" />
        <DataGridCheckBoxColumn Header="Active" Binding="{Binding IsActive}" Width="80" />
        <DataGridTextColumn Header="Role" Binding="{Binding Role}" Width="*" />
    </DataGrid.Columns>
</DataGrid>
```

### Expander

```xml
<Expander Header="展开标题">
    <!-- 内容 -->
</Expander>

<Expander ExpandDirection="Up" Header="向上展开" IsExpanded="True" />
<Expander ExpandDirection="Right" Header="向右展开" />
<Expander ExpandDirection="Left" Header="向左展开" IsExpanded="True" />
```

### ListBox

```xml
<ListBox Width="200" Height="150" SelectionMode="Single">
    <ListBoxItem Content="Item One" />
    <ListBoxItem Content="Item Two" IsSelected="True" />
    <ListBoxItem Content="Item Three" />
</ListBox>
```

### TreeView

```xml
<TreeView Width="250" Height="200">
    <TreeViewItem Header="Root">
        <TreeViewItem Header="Child 1">
            <TreeViewItem Header="Grandchild 1.1" />
        </TreeViewItem>
    </TreeViewItem>
</TreeView>
```

### Menu

```xml
<Menu>
    <MenuItem Header="File">
        <MenuItem Header="New" />
        <MenuItem Header="Open" />
        <Separator />
        <MenuItem Header="Exit" />
    </MenuItem>
</Menu>
```

### TabControl

```xml
<TabControl>
    <TabItem Header="General">
        <TextBlock Text="内容" Margin="8" />
    </TabItem>
    <TabItem Header="Advanced">
        <TextBlock Text="内容" Margin="8" />
    </TabItem>
</TabControl>
```

### ProgressBar

```xml
<!-- 水平 -->
<ProgressBar Value="60" Width="400" ShowProgressText="True" />
<ProgressBar IsIndeterminate="True" Value="20" Width="400" />

<!-- 垂直 -->
<ProgressBar Value="20" Height="300" Orientation="Vertical" ShowProgressText="True" />
<ProgressBar IsIndeterminate="True" Height="300" Orientation="Vertical" />
```

### Slider

```xml
<Slider Width="400" Minimum="0" Maximum="100" Value="40" />
<Slider Orientation="Vertical" Minimum="0" Maximum="100" Value="40" />
```

### SplitView

```xml
<SplitView IsPaneOpen="True" OpenPaneLength="200" DisplayMode="CompactOverlay">
    <SplitView.Pane>
        <StackPanel>
            <TextBlock Text="导航" FontWeight="SemiBold" />
            <Button Content="Home" />
            <Button Content="Settings" />
        </StackPanel>
    </SplitView.Pane>
    <TextBlock Text="主内容" />
</SplitView>
```

`DisplayMode` 可选值：
- `Overlay` — 面板覆盖内容
- `Inline` — 面板内联，内容挤压
- `CompactInline` — 紧凑内联
- `CompactOverlay` — 紧凑覆盖

### ContextMenu

```xml
<Button Content="右键菜单">
    <Button.ContextMenu>
        <ContextMenu>
            <MenuItem Header="Copy" />
            <MenuItem Header="Paste" />
            <Separator />
            <MenuItem Header="Select All" />
        </ContextMenu>
    </Button.ContextMenu>
</Button>
```

### AutoCompleteBox

```xml
<AutoCompleteBox Width="200" FilterMode="Contains" Watermark="Search..." />
```

```csharp
autoCompleteBox.ItemsSource = new List<string>
{
    "Avalonia", "Button", "CheckBox", "ComboBox",
    // ...
};
```

### RepeatButton

```xml
<RepeatButton Content="-" Name="DecrementBtn" />
<TextBlock Name="ValueText" Text="0" />
<RepeatButton Content="+" Name="IncrementBtn" />
```

```csharp
int counter = 0;
incrementBtn.Click += (_, _) => { counter++; ValueText.Text = counter.ToString(); };
decrementBtn.Click += (_, _) => { counter--; ValueText.Text = counter.ToString(); };
```

## 附加属性

### Badge (徽标)

附加属性方式，直接绑定到任意 Control：

```xml
<!-- 数字徽标 -->
<Button wd:Badge.BadgeText="5" Content="Messages" />
<Button wd:Badge.BadgeText="99+" Content="Notifications" />

<!-- 圆点徽标 -->
<Button wd:Badge.IsDot="True" Content="Dot Badge" />

<!-- 自定义颜色 -->
<Button wd:Badge.BadgeText="3"
        wd:Badge.BadgeBackground="#06D6A0"
        wd:Badge.BadgeForeground="White"
        wd:Badge.BadgeFontSize="12"
        Content="Custom" />
```

C# 中使用：

```csharp
// 设置徽标
var btn = new Button { Content = "Messages" };
Badge.SetBadgeText(btn, "5");

// 圆点徽标
Badge.SetIsDot(btn, true);

// 自定义颜色
Badge.SetBadgeBackground(btn, Brushes.Green);
Badge.SetBadgeForeground(btn, Brushes.White);

// 移除徽标
Badge.SetBadgeText(btn, null);
Badge.SetIsDot(btn, false);
```

可用附加属性：
| 属性 | 类型 | 说明 | 默认值 |
|------|------|------|--------|
| `BadgeText` | `string?` | 徽标文字 | `null` |
| `IsDot` | `bool` | 圆点模式 | `false` |
| `BadgeBackground` | `IBrush?` | 徽标背景色 | `WD.DangerBrush` |
| `BadgeForeground` | `IBrush?` | 徽标文字颜色 | `White` |
| `BadgeFontSize` | `double` | 徽标字号 | `10` |

### Loading (加载遮罩)

```xml
<Label Content="加载中" wd:Loading.IsShow="True" />
<Button Content="加载中" wd:Loading.IsShow="True" />
```

### Mask (遮罩层)

```xml
<Label Content="遮罩" wd:Mask.IsShow="True" />
```

## Toast

代码中调用，支持四种类型：

```csharp
Toast.Push("提示信息", ToastIcon.Info);
Toast.Push("操作成功", ToastIcon.Success);
Toast.Push("警告信息", ToastIcon.Warning);
Toast.Push("错误信息", ToastIcon.Error);

// 清除所有 Toast
Toast.Clear();
```

Toast 在 10 秒后自动消失，也可手动关闭。

## MessageBox

```csharp
// 信息提示
MessageBox.Show("这是一条信息", "标题", MessageBoxImage.Information, window);

// 警告
MessageBox.Show("请注意", "警告", MessageBoxImage.Warning, window);

// 错误
MessageBox.Show("出错了", "错误", MessageBoxImage.Error, window);

// Yes/No 确认
MessageBox.Show("确定吗？", "确认", MessageBoxButton.YesNo, MessageBoxImage.Question, window);

// Yes/No/Cancel 异步
var result = await MessageBox.Show("保存更改？", "确认", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, window);
if (result == MessageBoxResult.Yes)
{
    // 处理
}
```

## Label & Separator

```xml
<Label Content="Primary" Foreground="{DynamicResource WD.PrimaryBrush}" />
<Label Content="Regular" />
<Label Content="HyperlinkButton">
    <Label.ContentTemplate>
        <DataTemplate>
            <HyperlinkButton Content="{Binding}" NavigateUri="https://example.com" />
        </DataTemplate>
    </Label.ContentTemplate>
</Label>

<Separator Margin="0,12" />
```

## 动态资源引用

颜色/画刷：

```xml
Foreground="{DynamicResource WD.PrimaryBrush}"       <!-- 主题色 -->
Foreground="{DynamicResource WD.SuccessBrush}"        <!-- 成功绿 -->
Foreground="{DynamicResource WD.WarningBrush}"        <!-- 警告黄 -->
Foreground="{DynamicResource WD.DangerBrush}"         <!-- 危险红 -->
Foreground="{DynamicResource WD.RegularTextBrush}"    <!-- 常规文字 -->
Foreground="{DynamicResource WD.PlaceholderTextBrush}"<!-- 占位文字 -->
Background="{DynamicResource WD.BackgroundBrush}"     <!-- 背景色 -->
BorderBrush="{DynamicResource WD.BaseBrush}"          <!-- 基础边框色 -->
```

## 颜色主题

内置 8 种主题色可供选择，运行时动态切换：

| 颜色 | 色值 |
|------|------|
| 玫瑰红 | #FF4D6D |
| 珊瑚橙 | #FF6D3A |
| 向日葵黄 | #FFD166 |
| 草绿色 | #06D6A0 |
| 天空蓝 | #118AB2 |
| 薰衣草紫 | #9B5DE5 |
| 樱花粉 | #FFB7B2 |
| 薄荷绿 | #A7E0E0 |

切换代码：
```csharp
((Resources)Application.Current.Resources).Color = Color.Parse("#FF4D6D");
```
