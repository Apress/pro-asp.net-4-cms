namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// Interface that HTML rendering-override plugins are expected to implement.
    /// </summary>
    public interface IRenderPlugin
    {
        string ModifyOutput(string page);
    }
}
