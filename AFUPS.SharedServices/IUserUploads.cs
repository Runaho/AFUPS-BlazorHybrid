using AFUPS.Data;

namespace AFUPS.SharedServices
{
    public interface IUserUploads
    {
        List<UserUpload> Get();
        UserUpload Get(int id);
        UserUpload GetUserUploadByArchiveId(int ArchiveId);
        string uploadedTotalSize();
        void Set(UserUpload Upload);
        void Remove(int id);
    }
}

