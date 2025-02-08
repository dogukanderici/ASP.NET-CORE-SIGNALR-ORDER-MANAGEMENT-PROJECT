namespace SignalRWebUI.Utilities.FileOperations
{
    public interface IFileOperation
    {
        Task<string> SaveFileAsync(FileSettings fileSettings);
    }
}
