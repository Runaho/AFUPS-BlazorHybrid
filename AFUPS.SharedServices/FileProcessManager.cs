using AFUPS.Data;
using AFUPS.Data.APIModels;
using ByteSizeLib;
using Microsoft.AspNetCore.Components.Forms;

namespace AFUPS.SharedServices
{
    public class FileProcessManager : IFileProcessManager
    {
        private List<FileConvertingProcess> _FCP { get; set; } = new List<FileConvertingProcess>();

        private ProcessManagerState ManagerState { get; set; } = ProcessManagerState.Waiting;

        private UserUpload userUpload = new UserUpload();
        private string ProcessGuid = Guid.NewGuid().ToString();

        private ArchiveFile GeneratedArchive { get; set; }
        private UserArchive UserArchive { get; set; }

        public UploadResult GetUploadResult() => userUpload.APIResult;
        public IUserArchives UserArchives { get; }
        public IUserUploads UserUploads { get; }
        public IUploaderProviders UploaderProviders { get; }

        public string ArchiveName { get; set; }

        public void SetArchiveName(string archiveName)
        {
            ArchiveName = archiveName;
        }

        public FileProcessManager(IUserArchives userArchives, IUserUploads userUploads, IUploaderProviders uploaderProviders)
        {
            UserArchives = userArchives;
            UserUploads = userUploads;
            UploaderProviders = uploaderProviders;
            GeneratedArchive = new ArchiveFile();
        }

        public void SetGeneratedArchive(UserArchive userArchive, ArchiveFile archiveFile, string _ProcessGuid)
        {
            GeneratedArchive = archiveFile;
            ProcessGuid = _ProcessGuid;
            UserArchive = userArchive;
        }

        public void Reset()
        {
            ProcessGuid = Guid.NewGuid().ToString();
            userUpload = new UserUpload();
            _FCP = new List<FileConvertingProcess>();
            ManagerState = ProcessManagerState.Waiting;
        }

        public async Task AddFileAsync(IBrowserFile browserFile)
        {
            var BrowserFileToMemoryStream = new BrowserFileToMemoryStream();

            ManagerState = ProcessManagerState.Converting;

            string guid = Guid.NewGuid().ToString();

            BrowserFileToMemoryStream.fileConvertingProcess = new FileConvertingProcess(browserFile.Name, browserFile.Size, guid);
            _FCP.Add(BrowserFileToMemoryStream.fileConvertingProcess);

            var softRef = _FCP.First(f => f.Guid == guid);

            await BrowserFileToMemoryStream.FileConvertingAsync(browserFile);

            if (softRef.Bytes == null)
            {
                error += $"{softRef.FileName} : The file could not be read.";
                _FCP.Remove(softRef);
            }

            ManagerState = ProcessManagerState.Converted;
        }

        public async Task AddFilesAsync(IReadOnlyList<IBrowserFile> browserFiles)
        {
            ManagerState = ProcessManagerState.Converting;

            foreach (var file in browserFiles)
            {
                await AddFileAsync(file);
            }

            ManagerState = ProcessManagerState.Converted;
        }

        public string Error() => error;
        private string error;

        public bool FCPHasFile()
        {
            return _FCP.Any();
        }

        public async Task GenerateArchiveAsync()
        {
            ManagerState = ProcessManagerState.Archiving;

            var archiveBytes = Archiver.GenerateArchive(_FCP);

            GeneratedArchive = new ArchiveFile { ArchiveBytes = archiveBytes, Guid = ProcessGuid };

            var _archiveName = ArchiveName ?? ProcessGuid;

            UserArchive = UserArchives.Set(GeneratedArchive, $"{_archiveName}.zip");

            ManagerState = ProcessManagerState.Archived;
        }

        public IEnumerable<FileConvertingProcess> GetFiles() => _FCP.AsEnumerable();

        public ArchiveFile? GetGeneratedArchive() => GeneratedArchive;

        public ProcessManagerState GetState() => ManagerState;

        public bool IsCanBeUploaded() => (IsConverting() is false
                                        && FCPHasFile() is true
                                        && ManagerState is ProcessManagerState.Archived);

        public bool IsConverting() => _FCP.Any(a => a.ConvertingPercentage != 100);

        public string ReadeableTotalConvertedFileSize() => ByteSize.FromBytes(_FCP.Sum(s => s.Size)).ToString();

        public void RemoveFile(FileConvertingProcess file)
        {
            _FCP.Remove(file);
        }

        public async Task StartUploadAsync(int providerId, string ArchiveName)
        {
            string archiveName = "";

            if (string.IsNullOrEmpty(ArchiveName))
            {

                if (UserArchive != null)
                    archiveName = UserArchive.Name;
            }
            else
            {
                archiveName = ArchiveName;
            }

            if (providerId is 0)
                return;

            var provider = UploaderProviders.GetUploaderProvider(providerId);

            if (provider is null)
                return;

            ManagerState = ProcessManagerState.Uploading;
            userUpload.ProviderId = providerId;
            await UploadAsync(GeneratedArchive.ArchiveBytes, archiveName, provider.Endpoint);
        }

        public UserUpload UploadDetails()
        {
            return userUpload;
        }

        public async Task UploadAsync(byte[] archiveBytes, string name, string endpoint)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = $"{ProcessGuid}.zip";
            }

            using (var client = new HttpClient())
            {
                try
                {
                    var byteContent = new ByteArrayContent(archiveBytes);
                    var multipartContent = new MultipartFormDataContent
                    {
                        { byteContent, "file", name }
                    };
                    var response = await client.PostAsync(endpoint, multipartContent);
                    userUpload.APIResult = await response.Content.ReadAsAsync<UploadResult>();
                    userUpload.Name = name;
                    userUpload.ArchiveID = (int)UserArchive.ArchiveID;
                    UserUploads.Set(userUpload);
                    UserArchive.UploadedDateTime = DateTime.Now;
                    UserArchives.Set(UserArchive);
                }
                catch (Exception ex)
                {
                    // Locking the application there is a problem.
                }
                ManagerState = ProcessManagerState.Ended;
            }

        }
    }

}


