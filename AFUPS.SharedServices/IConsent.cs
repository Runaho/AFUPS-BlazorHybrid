using System;
namespace AFUPS.SharedServices
{
	public interface IConsent
	{
        void ConsentAccepted();
        bool ConsentIsAccepted();
    }
}

