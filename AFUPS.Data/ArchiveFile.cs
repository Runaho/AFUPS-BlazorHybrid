using System;
using ByteSizeLib;
using SQLite;

namespace AFUPS.Data
{
    public class ArchiveFile
    {
        [PrimaryKey, AutoIncrement]
        public int? ArchiveFileID { get; set; }

        public byte[] ArchiveBytes { get; set; }

        public string Guid { get; set; }

        public string ReadableSize => ByteSize.FromBytes((double)ArchiveBytes.Length).ToString();
    }
}

