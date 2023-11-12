namespace WindowChromeMultiscreen.Demo;

public partial class MainWindow : CustomChromeWindow
{
    public MainWindow()
    {
        DataContext = new MainViewModel();
        InitializeComponent();
    }
}
