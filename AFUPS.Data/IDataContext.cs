using System;
namespace AFUPS.Data
{
    public interface IDataContext
    {
        List<UserArchive> GetUserArchives();
        UserArchive GetUserArchive(int id);
        void SetUserArchive(UserArchive Archive);
        void RemoveUserArchive(UserArchive Archive);
        int? UserArchiveFileId(string ArchiveGuid);

        List<UserUpload> GetUserUploads();
        UserUpload GetUserUpload(int id);
        UserUpload GetUserUploadByArchiveId(int ArchiveId);
        void SetUserUpload(UserUpload Upload);
        void RemoveUserUpload(int id);

        ArchiveFile? GetArchiveFile(UserArchive archive);
        UserArchive SetArchiveFile(ArchiveFile archiveFile,string archiveName);
        void RemoveArchiveFile(ArchiveFile archiveFile);

        List<UploaderProvider> GetUploaderProviders();
        UploaderProvider GetUploaderProvider(int id);
        void SetUploaderProvider(UploaderProvider provider);
        void RemoveUploaderProvider(int id);

        string SaveUser(string user);
        string GetUser();


        void ConsentAccepted();
        bool ConsentIsAccepted();
    }
}

