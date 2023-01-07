using System;
namespace AFUPS.Data
{
    public enum ProcessManagerState
    {
        Waiting,
        Selecting,
        Converting,
        Converted,
        Archiving,
        Archived,
        Uploading,
        Ended
    }
}

