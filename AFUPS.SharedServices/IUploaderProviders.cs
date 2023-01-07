using AFUPS.Data;

namespace AFUPS.SharedServices
{
    public interface IUploaderProviders
    {
        List<UploaderProvider> GetUploaderProviders();
        UploaderProvider GetUploaderProvider(int id);
        void SetUploaderProvider(UploaderProvider provider);
        void RemoveUploaderProvider(int id);
    }
}

