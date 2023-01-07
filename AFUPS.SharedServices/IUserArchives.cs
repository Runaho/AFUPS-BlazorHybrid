using AFUPS.Data;

namespace AFUPS.SharedServices
{
    public interface IUserArchives
    {
        List<UserArchive> Get();
        string ArchivesTotalSize();
        List<UserArchive> NotUploadedArchives();
        UserArchive Get(int id);
        UserArchive Set(ArchiveFile archiveFile, string fileName);
        void Set(UserArchive userArchive);
        int ArchiveCount();

        void RemoveUserArchive(UserArchive Archive);

        ArchiveFile? GetArchiveFile(UserArchive archive);
        void RemoveArchiveFile(ArchiveFile archiveFile);
    }
}