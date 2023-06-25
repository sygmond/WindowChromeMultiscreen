namespace WindowChromeMultiscreen.UI.Interfaces;

public interface IAppState
{
    void Track<T>(T item);
    void SaveAll();
}