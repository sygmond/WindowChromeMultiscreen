using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using WindowChromeMultiscreen.Core;

namespace WindowChromeMultiscreen;

[TemplatePart(Name = "PART_MinimizeButton", Type = typeof(TitlebarButton))]
[TemplatePart(Name = "PART_MaximizeButton", Type = typeof(TitlebarButton))]
[TemplatePart(Name = "PART_RestoreButton", Type = typeof(TitlebarButton))]
[TemplatePart(Name = "PART_CloseButton", Type = typeof(TitlebarButton))]
public class CustomChromeWindow : Window
{
    private TitlebarButton? minimizeButton;
    private TitlebarButton? maximizeButton;
    private TitlebarButton? restoreButton;
    private TitlebarButton? closeButton;

    public CustomChromeWindow()
    {
        StateChanged += OnStateChanged;
        MouseLeftButtonDown += (_, _) => { DragMove(); };
    }

    static CustomChromeWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomChromeWindow), new FrameworkPropertyMetadata(typeof(CustomChromeWindow)));
    }

    private void OnStateChanged(object? sender, System.EventArgs e)
    {
        RefreshMaximizeRestoreButton();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        ((HwndSource)PresentationSource.FromVisual(this))?.AddHook(NativeMethods.HookProc);

        //_appState.Track(this);
    }

    private void RefreshMaximizeRestoreButton()
    {
        var isMaximized = WindowState == WindowState.Maximized;

        if (maximizeButton != null)
        {
            maximizeButton.Visibility = isMaximized ? Visibility.Collapsed : Visibility.Visible;
        }

        if (restoreButton != null)
        {
            restoreButton.Visibility = isMaximized ? Visibility.Visible : Visibility.Collapsed;
        }
    }


    public override void OnApplyTemplate()
    {
        minimizeButton = GetTemplateChild("PART_MinimizeButton") as TitlebarButton;
        maximizeButton = GetTemplateChild("PART_MaximizeButton") as TitlebarButton;
        restoreButton = GetTemplateChild("PART_RestoreButton") as TitlebarButton;
        closeButton = GetTemplateChild("PART_CloseButton") as TitlebarButton;

        if (minimizeButton != null)
        {
            minimizeButton.Click += OnMinimizeButtonClicked;
        }
        if (maximizeButton != null)
        {
            maximizeButton.Click += OnMaximizeButtonClicked;
        }
        if (restoreButton != null)
        {
            restoreButton.Click += OnRestoreButtonClicked;
        }
        if (closeButton != null)
        {
            closeButton.Click += OnCloseButtonClicked;
        }

        RefreshMaximizeRestoreButton();

        base.OnApplyTemplate();
    }

    private void OnCloseButtonClicked(object sender, RoutedEventArgs e)
    {
        OnClosing();
        Close();
    }

    protected virtual void OnClosing() { }

    private void OnRestoreButtonClicked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Normal;
        if (restoreButton != null)
        {
            restoreButton.Visibility = Visibility.Hidden;
        }

        if (maximizeButton != null)
        {
            maximizeButton.Visibility = Visibility.Visible;
        }
    }

    private void OnMaximizeButtonClicked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Maximized;
        if (restoreButton != null)
        {
            restoreButton.Visibility = Visibility.Visible;
        }

        if (maximizeButton != null)
        {
            maximizeButton.Visibility = Visibility.Hidden;
        }
    }

    private void OnMinimizeButtonClicked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    public static readonly DependencyProperty TitlebarHeightProperty = DependencyProperty.Register("TitlebarHeight", typeof(double), typeof(CustomChromeWindow), new PropertyMetadata(48.0));
    public static readonly DependencyProperty TitleHorizontalAlignmentProperty = DependencyProperty.Register("TitleHorizontalAlignment", typeof(HorizontalAlignment), typeof(CustomChromeWindow), new PropertyMetadata(HorizontalAlignment.Left));
    public static readonly DependencyProperty CenterAreaProperty = DependencyProperty.Register("CenterArea", typeof(List<FrameworkElement>), typeof(CustomChromeWindow), new PropertyMetadata(new List<FrameworkElement>()));
    public static readonly DependencyProperty BeforeTitleAreaProperty = DependencyProperty.Register("BeforeTitleArea", typeof(List<FrameworkElement>), typeof(CustomChromeWindow), new PropertyMetadata(new List<FrameworkElement>()));
    public static readonly DependencyProperty AfterTitleAreaProperty = DependencyProperty.Register("AfterTitleArea", typeof(List<FrameworkElement>), typeof(CustomChromeWindow), new PropertyMetadata(new List<FrameworkElement>()));

    public double TitlebarHeight
    {
        get { return (double)GetValue(TitlebarHeightProperty); }
        set { SetValue(TitlebarHeightProperty, value); }
    }
    public HorizontalAlignment TitleHorizontalAlignment
    {
        get { return (HorizontalAlignment)GetValue(TitleHorizontalAlignmentProperty); }
        set { SetValue(TitleHorizontalAlignmentProperty, value); }
    }
    public List<Control> CenterArea
    {
        get { return (List<Control>)GetValue(CenterAreaProperty); }
        set { SetValue(CenterAreaProperty, value); }
    }
    public List<FrameworkElement> BeforeTitleArea
    {
        get { return (List<FrameworkElement>)GetValue(BeforeTitleAreaProperty); }
        set { SetValue(BeforeTitleAreaProperty, value); }
    }
    public List<FrameworkElement> AfterTitleArea
    {
        get { return (List<FrameworkElement>)GetValue(AfterTitleAreaProperty); }
        set { SetValue(AfterTitleAreaProperty, value); }
    }
}

