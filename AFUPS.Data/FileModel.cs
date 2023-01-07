using System;
using ByteSizeLib;

namespace AFUPS.Data
{
    public class FileModel
    {
        public string Name { get; set; }
        public long Length { get; set; }

        public string ReadableSize => ByteSize.FromBytes((double)Length).ToString();
    }
}

