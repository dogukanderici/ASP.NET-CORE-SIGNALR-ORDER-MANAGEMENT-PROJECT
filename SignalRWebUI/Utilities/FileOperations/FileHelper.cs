
namespace SignalRWebUI.Utilities.FileOperations
{
    public class FileHelper : IFileOperation
    {
        public async Task<string> SaveFileAsync(FileSettings fileSettings)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(fileSettings.UploadFile.FileName);
            var uploadedFileName = Guid.NewGuid().ToString() + extension;

            CreateFolder(fileSettings.FolderPath, fileSettings.FolderName);

            var saveLoaction = resource + fileSettings.FolderPath + "\\" + fileSettings.FolderName + "\\" + uploadedFileName;
            var stream = new FileStream(saveLoaction, FileMode.Create);

            await fileSettings.UploadFile.CopyToAsync(stream);

            return uploadedFileName;
        }

        private bool IsFolderExitsAsync(string folderPath, string folderName)
        {
            string path = Directory.GetCurrentDirectory() + folderPath + "\\" + folderName;
            bool isCheck = Directory.Exists(path);

            return isCheck;
        }

        private void CreateFolder(string folderPath, string folderName)
        {
            bool isFolderExists = IsFolderExitsAsync(folderPath, folderName);

            if (!isFolderExists)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + folderPath + "\\" + folderName);
            }
        }
    }
}
