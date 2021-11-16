using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSC.Intefaces;

namespace WSC
{
	public partial class mainView : Form, ImainView
	{
		//closing form event
		public event OnClosingForm CloseForm;

		//add new ws event
		public event OnAddNewWebSite AddNewWebSite;

		//delete website button pressed
		public event OnDeleteWebSite DeleteWebSite;

		//edit website button pressed
		public event OnEditWebSite EditWebSite;

		//on user clicked on row
		public event OnSelectingItem SelectionItem;

		WebSite selectedWebSite = null;

		public mainView()
		{
			InitializeComponent();
		}

		//make website list Name | Status
		void ImainView.ShowUserWS(IEnumerable<WebSite> webSites)
		{
			WebSitesListBox.Items.Clear();
			foreach (WebSite currentwebsite in webSites)
			{
				WebSitesListBox.Items.Add(
					currentwebsite.webSiteName + "\t"  + currentwebsite.workStatus.ToString());
			}
		}

		//if form closed - stop checking availability
		private void mainView_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (CloseForm != null)
				CloseForm();
		}

		public void UpdateView(int indexOfUpdated, string wsName, bool newStatus)
		{
			//use invoke to realize control 
			WebSitesListBox.Invoke((MethodInvoker)delegate
			{
				WebSitesListBox.Items.RemoveAt(indexOfUpdated);//remove row with old data
				//add new row with new data
				WebSitesListBox.Items.Insert(indexOfUpdated, wsName + "\t" + newStatus.ToString());
			});
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			//generate add event
			AddNewWebSite(this.WebSiteNameTextBox.Text,
				this.WebSiteUrlTextBox.Text, this.TimeIntervalTextBox.Text);
		}
		
		private void WebSitesListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//inform presentor that user selected item in list
			SelectionItem(WebSitesListBox.SelectedIndex);
		}

		void ImainView.ShowSelectedItemDataInInfoFields(WebSite selectedWebSite)
		{
			//put data to fields
			this.selectedWebSite = selectedWebSite;

			WebSiteNameTextBox.Text = selectedWebSite.webSiteName;
			WebSiteUrlTextBox.Text = selectedWebSite.webSiteUrl;
			TimeIntervalTextBox.Text = selectedWebSite.requestTimeInterval.ToString();
		}

		private void Deletebutton_Click(object sender, EventArgs e)
		{
			//generate delete event
			DeleteWebSite(selectedWebSite.webSiteId, WebSitesListBox.SelectedIndex);
		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			EditWebSite(WebSitesListBox.SelectedIndex,
				WebSiteNameTextBox.Text, WebSiteUrlTextBox.Text,
				TimeIntervalTextBox.Text);
		}
		public void SendUserMessage(string MessageText)
		{
			WebSitesListBox.Invoke((MethodInvoker)delegate
			{
				MessageTextBox.Text = MessageText;
			});
			
		}
	}
}
