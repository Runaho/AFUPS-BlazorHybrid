using System;
using AFUPS.Data;
using ByteSizeLib;

namespace AFUPS.SharedServices
{
    public class UserArchives : IUserArchives
    {
        public UserArchives(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IDataContext DataContext { get; }

        public int ArchiveCount() => Get().Count();

        public List<UserArchive> Get() => DataContext.GetUserArchives();

        public UserArchive Get(int id) => DataContext.GetUserArchive(id);

        public ArchiveFile? GetArchiveFile(UserArchive archive) => DataContext.GetArchiveFile(archive);

        public void RemoveUserArchive(UserArchive Archive) => DataContext.RemoveUserArchive(Archive);
        public void RemoveArchiveFile(ArchiveFile archiveFile) => DataContext.RemoveArchiveFile(archiveFile);
        public UserArchive Set(ArchiveFile archiveFile, string fileName) => DataContext.SetArchiveFile(archiveFile, fileName);
        public void Set(UserArchive userArchive) => DataContext.SetUserArchive(userArchive);

        public string ArchivesTotalSize()
        {
            var total = Get().Sum(s => s.FileSize);
            var size = new ByteSize(total);
            return size.ToString();
        }

        public List<UserArchive> NotUploadedArchives() => Get().Where(w => w.UploadedDateTime == null).ToList();
    }
}

