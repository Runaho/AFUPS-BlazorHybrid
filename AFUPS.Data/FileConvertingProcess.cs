using System;
using ByteSizeLib;

namespace AFUPS.Data
{
    public record FileConvertingProcess(string FileName, long Size, string Guid)
    {
        public long UploadedBytes { get; set; } = 0;
        public byte[]? Bytes { get; set; }
        public double ConvertingPercentage => (double)UploadedBytes / (double)Size * 100d;
        public string ReadableSize => ByteSize.FromBytes((double)Size).ToString();
        public string ReadableUploadedSize => ByteSize.FromBytes((double)UploadedBytes).ToString();
    }
}

