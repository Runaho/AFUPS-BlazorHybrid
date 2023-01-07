using System;
using SQLite;

namespace AFUPS.Data
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int? AuthUserId { get; set; }

        public string UserJson { get; set; }
    }
}

