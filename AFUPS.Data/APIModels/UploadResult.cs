using System;
using ByteSizeLib;

namespace AFUPS.Data.APIModels
{
    public class UploadResult
    {
        public bool status { get; set; }
        public Data? data { get; set; }

        public class Data
        {
            public File file { get; set; }
        }

        public class File
        {
            public Url url { get; set; }
            public Metadata metadata { get; set; }
        }

        public class Metadata
        {
            public string id { get; set; }
            public string name { get; set; }
            public Size size { get; set; }
        }

        public class Size
        {
            public int bytes { get; set; }
            public string readable { get; set; }
        }

        public class Url
        {
            public string full { get; set; }
            public string @short { get; set; }
        }

        public string ReadableSize()
        {
            var size = data?.file?.metadata?.size?.bytes;
            if (size == null)
                return "";

            return ByteSize.FromBytes((double)size).ToString();

        }
    }
}

