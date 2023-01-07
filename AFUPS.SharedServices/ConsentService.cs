using System;
using AFUPS.Data;

namespace AFUPS.SharedServices
{
    public class ConsentService : IConsent
    {
        public ConsentService(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IDataContext DataContext { get; }

        public void ConsentAccepted() => DataContext.ConsentAccepted();

        public bool ConsentIsAccepted() => DataContext.ConsentIsAccepted();
    }
}

