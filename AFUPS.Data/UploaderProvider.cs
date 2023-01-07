using System;
using SQLite;

namespace AFUPS.Data
{
    public class UploaderProvider
    {
        [PrimaryKey, AutoIncrement]
        public int ProviderId { get; set; }

        public string Name { get; set; }
        public string Logo { get; set; }
        public string Endpoint { get; set; }
        public string TermsUrl { get; set; }
    }
}

