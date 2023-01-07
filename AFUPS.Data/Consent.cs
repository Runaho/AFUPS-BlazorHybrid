using System;
using SQLite;

namespace AFUPS.Data
{
	public class Consent
	{
        [PrimaryKey]
        public int ConsentID { get; set; }

        public bool Accepted { get; set; }

        public DateTime? AcceptedDate { get; set; }
    }
}

