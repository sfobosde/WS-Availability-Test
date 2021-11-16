using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WSC.Intefaces
{
	delegate void OnUpdatingWebSiteStatusEventHandler(int id, string wsName, bool newStatus);

	delegate void OnErrorHandling(string messagetext);

	interface Imodel
	{
		//Load date from db
		void LoadWebSitesDataFromDB();

		//submit data on demand
		IEnumerable<WebSite> GetWebSiteList();

		//Stop after closing form
		void StopCheckingWebSites();

		//start checking with open app
		void RunAvailabilityChecking();

		void AddNewWebSiteDataToDB(string webSiteName, string webSiteUrl, string timeinterval);
		//remove from list and db
		void DeleteWebSiteFromDB(int webSiteID, int selectedIndex);

		//remove from list
		void DeleteWebSiteFromList(int webSiteId, int selectedIndex);

		void EditWebSiteToDB(int selectedIndex, string webSiteName, 
			string webSiteUrl, string timeinterval);

		void EditWebSiteList(int selectedIndex, string webSiteName,
			string webSiteUrl, string timeinterval);

		//if website status changed
		event OnUpdatingWebSiteStatusEventHandler UpdatingWebSiteStatus;

		event OnErrorHandling ErrorHandling;

		//return website object with id = selected index
		WebSite GetWebSiteBySelectedIndex(int selectedIndex);

	}
}
