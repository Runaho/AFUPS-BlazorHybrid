using System;
namespace AFUPS.SharedServices
{
    public interface IShareVia
    {
        Task ShareUri(string Uri);
    }
}
