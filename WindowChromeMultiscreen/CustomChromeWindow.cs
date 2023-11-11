using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WindowChromeMultiscreen;

public class CustomChromeWindow : Window
{
    static CustomChromeWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomChromeWindow), new FrameworkPropertyMetadata(typeof(CustomChromeWindow)));
    }

    public GridLength TitlebarHeight
    {
        get { return (GridLength)GetValue(TitlebarHeightProperty); }
        set { SetValue(TitlebarHeightProperty, value); }
    }

    public static readonly DependencyProperty TitlebarHeightProperty = DependencyProperty.Register("TitlebarHeight", typeof(GridLength), typeof(CustomChromeWindow), new PropertyMetadata(new GridLength(48)));

    public HorizontalAlignment TitleHorizontalAlignment
    {
        get { return (HorizontalAlignment)GetValue(TitleHorizontalAlignmentProperty); }
        set { SetValue(TitleHorizontalAlignmentProperty, value); }
    }

    public static readonly DependencyProperty TitleHorizontalAlignmentProperty = DependencyProperty.Register("TitleHorizontalAlignment", typeof(HorizontalAlignment), typeof(CustomChromeWindow), new PropertyMetadata(HorizontalAlignment.Left));

    public List<Control> CenterControls
    {
        get { return (List<Control>)GetValue(CenterControlsProperty); }
        set { SetValue(CenterControlsProperty, value); }
    }

    public static readonly DependencyProperty CenterControlsProperty = DependencyProperty.Register("CenterControls", typeof(List<Control>), typeof(CustomChromeWindow), new PropertyMetadata(new List<Control>()));
}

