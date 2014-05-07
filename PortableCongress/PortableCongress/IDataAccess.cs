using System;
using System.Collections.Generic;

namespace PortableCongress
{
	public interface IDataAccess
	{
		Politician LoadPolitician (int id);
		List<Politician> LoadAllPoliticans ();
        void SaveFavoriteBill (Bill bill);
        List<Bill> LoadFavoriteBills ();
        void DeleteFavoriteBill (int id);
	}
}