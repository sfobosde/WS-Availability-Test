using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSC.Intefaces
{
	public delegate void OnClosingForm();

	public delegate void OnAddNewWebSite(string webSiteName, 
		string webSiteUrl, string timeinterval);

	public delegate void OnSelectingItem(int indexOfSelected);

	public delegate void OnDeleteWebSite(int webSiteToDelete, int selectedIndex);

	public delegate void OnEditWebSite(int selectedIndex, string webSiteName,
		string webSiteUrl, string timeinterval);

	interface ImainView
	{
		//create table list with ws
		void ShowUserWS(IEnumerable<WebSite> webSites);

		//Generate When Form closing, and stop checking availability
		event OnClosingForm CloseForm;

		//apdate data on view
		void UpdateView(int id, string wsName, bool newStatus);

		void ShowSelectedItemDataInInfoFields(WebSite selectedWebSite);

		//use in catch to send eror messages
		void SendUserMessage(string MessageText);

		//generate when user pressed button to add new ws
		event OnAddNewWebSite AddNewWebSite;

		event OnSelectingItem SelectionItem;

		event OnDeleteWebSite DeleteWebSite;

		event OnEditWebSite EditWebSite;
	}
}
