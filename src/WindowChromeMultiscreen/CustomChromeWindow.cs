using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using WindowChromeMultiscreen.Core;

namespace WindowChromeMultiscreen;

[TemplatePart(Name = Part.MinimizeButton, Type = typeof(TitlebarButton))]
[TemplatePart(Name = Part.MaximizeButton, Type = typeof(TitlebarButton))]
[TemplatePart(Name = Part.RestoreButton, Type = typeof(TitlebarButton))]
[TemplatePart(Name = Part.CloseButton, Type = typeof(TitlebarButton))]
public class CustomChromeWindow : Window
{
    public static readonly DependencyProperty TitlebarHeightProperty = DependencyProperty.Register(
        nameof(TitlebarHeight),
        typeof(double),
        typeof(CustomChromeWindow),
        new PropertyMetadata(48.0));

    public static readonly DependencyProperty TitleHorizontalAlignmentProperty = DependencyProperty.Register(
        nameof(TitleHorizontalAlignment),
        typeof(HorizontalAlignment),
        typeof(CustomChromeWindow),
        new PropertyMetadata(HorizontalAlignment.Left));

    public static readonly DependencyProperty CenterAreaProperty = DependencyProperty.Register(
        nameof(CenterArea),
        typeof(List<FrameworkElement>),
        typeof(CustomChromeWindow),
        new PropertyMetadata(new List<FrameworkElement>()));

    public static readonly DependencyProperty BeforeTitleAreaProperty = DependencyProperty.Register(
        nameof(BeforeTitleArea),
        typeof(List<FrameworkElement>),
        typeof(CustomChromeWindow),
        new PropertyMetadata(new List<FrameworkElement>()));

    public static readonly DependencyProperty AfterTitleAreaProperty = DependencyProperty.Register(
        nameof(AfterTitleArea),
        typeof(List<FrameworkElement>),
        typeof(CustomChromeWindow),
        new PropertyMetadata(new List<FrameworkElement>()));

    private TitlebarButton? _minimizeButton;
    private TitlebarButton? _maximizeButton;
    private TitlebarButton? _restoreButton;
    private TitlebarButton? _closeButton;

    public CustomChromeWindow()
    {
        StateChanged += OnStateChanged;
        MouseLeftButtonDown += (_, _) => { DragMove(); };
    }

    static CustomChromeWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomChromeWindow),
            new FrameworkPropertyMetadata(typeof(CustomChromeWindow)));
    }

    public override void OnApplyTemplate()
    {
        SubscribeToButtonsClickEvent();
        RefreshMaximizeRestoreButton();

        base.OnApplyTemplate();
    }

    public double TitlebarHeight
    {
        get => (double)GetValue(TitlebarHeightProperty);
        set => SetValue(TitlebarHeightProperty, value);
    }

    public HorizontalAlignment TitleHorizontalAlignment
    {
        get => (HorizontalAlignment)GetValue(TitleHorizontalAlignmentProperty);
        set => SetValue(TitleHorizontalAlignmentProperty, value);
    }

    public List<FrameworkElement> BeforeTitleArea
    {
        get => (List<FrameworkElement>)GetValue(BeforeTitleAreaProperty);
        set => SetValue(BeforeTitleAreaProperty, value);
    }

    public List<FrameworkElement> AfterTitleArea
    {
        get => (List<FrameworkElement>)GetValue(AfterTitleAreaProperty);
        set => SetValue(AfterTitleAreaProperty, value);
    }
    public List<Control> CenterArea
    {
        get => (List<Control>)GetValue(CenterAreaProperty);
        set => SetValue(CenterAreaProperty, value);
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        if (PresentationSource.FromVisual(this) as HwndSource is { } hwndSource)
            hwndSource.AddHook(NativeMethods.HookProc);

        //_appState.Track(this);
    }

    protected virtual void OnClosing()
    {
    }

    private static void SubscribeToButtonClickEvent(ButtonBase? button, RoutedEventHandler eventHandler)
    {
        if (button == null)
            return;

        button.Click += eventHandler;
    }

    private void SubscribeToButtonsClickEvent()
    {
        if (TryGetTemplateChild(Part.MinimizeButton, out _minimizeButton))
            SubscribeToButtonClickEvent(_minimizeButton, OnMinimizeButtonClicked);

        if (TryGetTemplateChild(Part.MaximizeButton, out _maximizeButton))
            SubscribeToButtonClickEvent(_maximizeButton, OnMaximizeButtonClicked);

        if (TryGetTemplateChild(Part.RestoreButton, out _restoreButton))
            SubscribeToButtonClickEvent(_restoreButton, OnRestoreButtonClicked);

        if (TryGetTemplateChild(Part.CloseButton, out _closeButton))
            SubscribeToButtonClickEvent(_closeButton, OnCloseButtonClicked);
    }

    private bool TryGetTemplateChild<T>(string templateChildName, out T? templateChild) where T : class
    {
        templateChild = GetTemplateChild(templateChildName) as T;

        return templateChild != null;
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        RefreshMaximizeRestoreButton();
    }

    private void RefreshMaximizeRestoreButton()
    {
        var isMaximized = WindowState == WindowState.Maximized;

        if (_maximizeButton != null)
            _maximizeButton.Visibility = isMaximized ? Visibility.Collapsed : Visibility.Visible;

        if (_restoreButton != null)
            _restoreButton.Visibility = isMaximized ? Visibility.Visible : Visibility.Collapsed;
    }

    private void OnCloseButtonClicked(object sender, RoutedEventArgs e)
    {
        OnClosing();
        Close();
    }

    private void OnRestoreButtonClicked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Normal;

        if (_restoreButton != null)
            _restoreButton.Visibility = Visibility.Hidden;

        if (_maximizeButton != null)
            _maximizeButton.Visibility = Visibility.Visible;
    }

    private void OnMaximizeButtonClicked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Maximized;

        if (_restoreButton != null)
            _restoreButton.Visibility = Visibility.Visible;

        if (_maximizeButton != null)
            _maximizeButton.Visibility = Visibility.Hidden;
    }

    private void OnMinimizeButtonClicked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}