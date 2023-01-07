using AFUPS.Data;
using AFUPS.Data.APIModels;
using Microsoft.AspNetCore.Components.Forms;

namespace AFUPS.SharedServices
{
    public interface IFileProcessManager
    {
        void SetArchiveName(string archiveName);
        ProcessManagerState GetState();
        void Reset();
        UploadResult GetUploadResult();

        Task AddFileAsync(IBrowserFile browserFile);

        Task AddFilesAsync(IReadOnlyList<IBrowserFile> browserFiles);

        void RemoveFile(FileConvertingProcess file);

        IEnumerable<FileConvertingProcess> GetFiles();

        bool FCPHasFile();

        bool IsConverting();

        string ReadeableTotalConvertedFileSize();

        ArchiveFile? GetGeneratedArchive();
        void SetGeneratedArchive(UserArchive userArchive, ArchiveFile archiveFile,string ProcessGuid);
        Task GenerateArchiveAsync();

        bool IsCanBeUploaded();

        Task StartUploadAsync(int providerId, string? ArchiveName = null);

        UserUpload UploadDetails();

        string Error();
    }
}

