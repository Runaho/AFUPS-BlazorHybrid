using System;
using SQLite;

namespace AFUPS.Data
{
    public class SqliteDataContext : IDataContext
    {
        private SQLiteConnection dataBase;

        public SqliteDataContext(bool IsMemory = false)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AFUPS.db");

            if (IsMemory)
                databasePath = "Data Source=AFUPS;Mode=Memory;Cache=Shared";

            try
            {
                dataBase = new SQLiteConnection(databasePath);

                dataBase.CreateTable<ArchiveFile>();
                dataBase.CreateTable<UserUpload>();
                dataBase.CreateTable<UserArchive>();
                dataBase.CreateTable<UploaderProvider>();
                dataBase.CreateTable<Consent>();
                dataBase.CreateTable<User>();

                if (!dataBase.Table<User>().Any())
                {
                    dataBase.Insert(new User { UserJson = "" });
                }

                if (!dataBase.Table<UploaderProvider>().Any())
                {
                    dataBase.InsertAll(DefaultUploaderProviders.UploaderProviders);
                }

                if (!dataBase.Table<Consent>().Any())
                {
                    dataBase.Insert(new Consent { Accepted = false, ConsentID = 1 });
                }
            }
            catch (Exception ex)
            {
                throw new FieldAccessException("SQLite File Cannot be created. " + ex.ToString());
            }

        }

        public ArchiveFile? GetArchiveFile(UserArchive archive)
        {
            return dataBase.Table<ArchiveFile>().FirstOrDefault(f => f.ArchiveFileID == archive.ArchiveFileId);
        }

        public UserArchive GetUserArchive(int id)
        {
            return dataBase.Table<UserArchive>().First(f => f.ArchiveID == id);
        }

        public List<UserArchive> GetUserArchives()
        {
            return dataBase.Table<UserArchive>().ToList();
        }

        public UserUpload GetUserUpload(int id)
        {
            return dataBase.Table<UserUpload>().FirstOrDefault(f => f.UploadID == id);
        }

        public UserUpload GetUserUploadByArchiveId(int ArchiveId)
        {
            return dataBase.Table<UserUpload>().LastOrDefault(f => f.ArchiveID == ArchiveId);
        }

        public List<UserUpload> GetUserUploads()
        {
            return dataBase.Table<UserUpload>().ToList();
        }

        /// <summary>
        /// Controls user archive record and returns founded archive file id
        /// </summary>
        /// <param name="userArchive">User Archive Record</param>
        /// <returns>ArchiveFileID</returns>
        public int? UserArchiveFileId(string ArchiveGuid)
        {
            var archiveFile = dataBase.Table<ArchiveFile>().FirstOrDefault(a => a.Guid == ArchiveGuid);
            if (archiveFile is null)
                return null;

            return archiveFile.ArchiveFileID;
        }

        public void RemoveArchiveFile(ArchiveFile archiveFile)
        {
            if (archiveFile != null)
                dataBase.Delete(archiveFile);
        }

        public void RemoveUserArchive(UserArchive Archive)
        {
            dataBase.Delete(Archive);
        }

        public void RemoveUserUpload(int id)
        {
            var upload = dataBase.Table<UserUpload>().FirstOrDefault(f => f.UploadID == id);
            if (upload is not null)
                dataBase.Delete(upload);
        }

        public UserArchive SetArchiveFile(ArchiveFile archiveFile, string archiveName)
        {
            dataBase.InsertOrReplace(archiveFile);
            var userArchive = new UserArchive { ArchiveFileId = archiveFile.ArchiveFileID, Name = archiveName, FileSize = archiveFile.ArchiveBytes.Length };
            SetUserArchive(userArchive);
            return userArchive;
        }

        public void SetUserArchive(UserArchive Archive)
        {
            dataBase.InsertOrReplace(Archive);
        }

        public void SetUserUpload(UserUpload Upload)
        {
            dataBase.InsertOrReplace(Upload);
        }

        public List<UploaderProvider> GetUploaderProviders() =>
            dataBase.Table<UploaderProvider>().ToList();

        public UploaderProvider GetUploaderProvider(int id) =>
            dataBase.Table<UploaderProvider>().FirstOrDefault(f => f.ProviderId == id);

        public void SetUploaderProvider(UploaderProvider provider) => dataBase.InsertOrReplace(provider);

        public void RemoveUploaderProvider(int id)
        {
            if (dataBase.Table<UserUpload>().Any(a => a.ProviderId == id))
                return;

            dataBase.Delete<UserUpload>(id);
        }

        public void ConsentAccepted()
        {
            var consent = dataBase.Table<Consent>().First();
            consent.Accepted = true;
            consent.AcceptedDate = DateTime.Now;
            dataBase.Update(consent);
        }

        public bool ConsentIsAccepted() => dataBase.Table<Consent>().First().Accepted;

        public string SaveUser(string user)
        {
            var _user = dataBase.Table<User>().First();
            _user.UserJson = user;
            return GetUser();
        }

        public string GetUser() => dataBase.Table<User>().First().UserJson;
    }
}

