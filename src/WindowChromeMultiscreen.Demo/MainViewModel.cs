using CommunityToolkit.Mvvm.ComponentModel;

namespace WindowChromeMultiscreen.Demo;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _searchValue = string.Empty;
}
