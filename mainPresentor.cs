using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSC.Intefaces;

namespace WSC
{
	class mainPresentor
	{
		Imodel webSitesModel;//model Control interface 
		ImainView mainView;//view control inteface

		IEnumerable<WebSite> wsList;// to transfer data from model to view
		WebSite SelectedWebSite = null; // selected in view website

		public mainPresentor(Imodel webSitesModel, ImainView mainView)
		{
			this.webSitesModel = webSitesModel;//connect model object as interface with presentor
			this.mainView = mainView;//connect view object as interface with presentor

			this.mainView.CloseForm += FormClosed();//form closing event handling

			//add handler of updating status event
			this.webSitesModel.UpdatingWebSiteStatus += UpdatingWebSiteStatus;

			//add handler of add new web site event
			this.mainView.AddNewWebSite += AddNewWebSite;

			//add handler to selecting item event
			this.mainView.SelectionItem += SelectingItem;

			//add handler to delete item
			this.mainView.DeleteWebSite += DeleteWebSite;

			//add handler to edit item
			this.mainView.EditWebSite += EditWebSite;

			this.webSitesModel.ErrorHandling += HandleErors;

			SendDataToView();//get data from model and send it to view
		}

		public void HandleErors(string messagetext)
		{
			mainView.SendUserMessage(messagetext);
		}

		//get data from model and send it to view
		private void SendDataToView()
		{
			try
			{
				//load websites data from model
				wsList = webSitesModel.GetWebSiteList();

				//Send data to View
				mainView.ShowUserWS(wsList);
			}
			catch(Exception ex)
			{
				HandleErors(ex.Message);
			}
		}
		//form closing event handling
		public OnClosingForm FormClosed()
		{
			webSitesModel.StopCheckingWebSites();//stop checking web sites on availability
			return null;
		}

		//call updating view
		public void UpdatingWebSiteStatus(int id, string wsName, bool newStatus)
		{
			mainView.UpdateView(id, wsName, newStatus);
		}

		//add new website
		public void AddNewWebSite(string webSiteName,
			string webSiteUrl, string timeinterval)
		{
			//check data type matching
			int result;	
			if(int.TryParse(timeinterval, out result))
			{
				//submit data to add to model and db
				webSitesModel.AddNewWebSiteDataToDB(webSiteName, webSiteUrl, timeinterval);

				//load websites data from model
				wsList = webSitesModel.GetWebSiteList();

				//Send data to View
				mainView.ShowUserWS(wsList);

				//restat checking availability
				webSitesModel.StopCheckingWebSites();
				webSitesModel.RunAvailabilityChecking();
			}
			else
			{
				HandleErors("Не соотвествие типов данных");
			}
		}

		public void SelectingItem(int selectedIndex)
		{
			//get object from model
			SelectedWebSite = webSitesModel.GetWebSiteBySelectedIndex(selectedIndex);
			//submit object to view
			mainView.ShowSelectedItemDataInInfoFields(SelectedWebSite);
		}
		//on deleting WS handler
		public void DeleteWebSite(int websiteIdToDelete, int selectedIndex)
		{
			//delete fron db
			webSitesModel.DeleteWebSiteFromDB(websiteIdToDelete, selectedIndex);
			//delete fron list
			webSitesModel.DeleteWebSiteFromList(websiteIdToDelete, selectedIndex);
			//update view
			wsList = webSitesModel.GetWebSiteList();

			//Send data to View
			mainView.ShowUserWS(wsList);
		}
		public void EditWebSite(int selectedIndex, string webSiteName,
			string webSiteUrl, string timeinterval)
		{
			//save changes to db
			webSitesModel.EditWebSiteToDB(selectedIndex, webSiteName, webSiteUrl, timeinterval);
			//save changes to list
			webSitesModel.EditWebSiteList(selectedIndex, webSiteName, webSiteUrl, timeinterval);

			//update view
			wsList = webSitesModel.GetWebSiteList();

			//Send data to View
			mainView.ShowUserWS(wsList);
		}
	}
}
