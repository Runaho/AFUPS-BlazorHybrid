using System;
using AFUPS.Data;

namespace AFUPS.SharedServices
{
    public class UploaderProviders : IUploaderProviders
    {

        public UploaderProviders(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IDataContext DataContext { get; }

        public UploaderProvider GetUploaderProvider(int id) => DataContext.GetUploaderProvider(id);

        public List<UploaderProvider> GetUploaderProviders() => DataContext.GetUploaderProviders();

        public void RemoveUploaderProvider(int id) => DataContext.RemoveUploaderProvider(id);

        public void SetUploaderProvider(UploaderProvider provider) => DataContext.SetUploaderProvider(provider);
    }
}
