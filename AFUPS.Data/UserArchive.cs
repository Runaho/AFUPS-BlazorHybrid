using System;
using ByteSizeLib;
using SQLite;

namespace AFUPS.Data
{
    public class UserArchive
    {
        [PrimaryKey, AutoIncrement]
        public int? ArchiveID { get; set; }
        public int? ArchiveFileId { get; set; }
        public string Name { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime? UploadedDateTime { get; set; }

        public int FileSize { get; set; }
        public string ReadableSize => ByteSize.FromBytes((double)FileSize).ToString();
    }
}

