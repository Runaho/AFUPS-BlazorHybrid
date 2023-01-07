using System;
using AFUPS.Data;
using ByteSizeLib;

namespace AFUPS.SharedServices
{
    public class UserUploads : IUserUploads
    {
        public UserUploads(IDataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public IDataContext dataContext { get; }

        public void Set(UserUpload Upload)
        {
            dataContext.SetUserUpload(Upload);
        }

        public List<UserUpload> Get() => dataContext.GetUserUploads();

        public UserUpload Get(int id) => dataContext.GetUserUpload(id);

        public UserUpload GetUserUploadByArchiveId(int ArchiveId) => dataContext.GetUserUploadByArchiveId(ArchiveId);

        public void Remove(int id)
        {
            dataContext.RemoveUserUpload(id);
        }

        public string uploadedTotalSize()
        {
            var userUploads = Get();
            var total = userUploads.Sum(s => s.TotalSize);
            var size = new ByteSize(total);
            return size.ToString();
        }
    }
}


