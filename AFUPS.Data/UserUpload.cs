using System;
using ByteSizeLib;
using SQLite;
using System.Text.Json;
using AFUPS.Data.APIModels;

namespace AFUPS.Data
{
    public class UserUpload
    {
        [PrimaryKey, AutoIncrement,Indexed]
        public int? UploadID { get; set; }
        public int ArchiveID { get; set; }

        [Ignore]
        public string MetaID => APIResult?.data.file.metadata.id;
        public string Name { get; set; }
        public int ProviderId { get; set; }
        public DateTime LiveDate { get; set; } = DateTime.Now;

        [Ignore]
        public int TotalSize => (int)(APIResult?.data.file.metadata.size.bytes);

        [Column("APIResult")]
        public string? _APIResult { get; set; }

        [Ignore]
        public string ReadableSize => ByteSize.FromBytes((double)TotalSize).ToString();

        [Ignore]
        public string DownloadUrl => APIResult?.data?.file?.url.full;

        private UploadResult result;

        [Ignore]
        public UploadResult? APIResult
        {
            get
            {
                if (_APIResult == null)
                    return new UploadResult { };

                return result ??= JsonSerializer.Deserialize<UploadResult>(_APIResult);
            }
            set
            {
                _APIResult = JsonSerializer.Serialize(value).ToString();
            }
        }


    }
}

