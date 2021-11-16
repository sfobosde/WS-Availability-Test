using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSC.Intefaces;
using System.Data.SqlClient;
using System.Net;
using System.Windows.Forms;

namespace WSC
{
	class Model: Imodel
	{
		static SqlConnection dataBaseConnect; //store sqlserver connection string

		SqlDataReader sqlDataReader;
		SqlCommand sqlCommand;

		WebRequest requestFromWS;//reqfuest object
		HttpWebResponse webResponse;//response object

		IList<WebSite> LoadedWSList = new List<WebSite>();

		bool IsChekicngStarted;//thread IsAlive status

		//if web site change availability generate event
		public event OnUpdatingWebSiteStatusEventHandler UpdatingWebSiteStatus;

		public event OnErrorHandling ErrorHandling;

		public Model()
		{
			//create connection source to Server WSCheckDB database
			dataBaseConnect =
				new SqlConnection(@"Data Source=LAPTOP-D01LT47H\SQLEXPRESS;
				Initial Catalog=WSCheckDB; Integrated Security=True"); //store sqlserver connection string

			//Select data from DB to LoadedWSList
			LoadWebSitesDataFromDB();

			//Start Checking WS
			RunAvailabilityChecking();
		}
		public void OpenConnection()
		{
			//check is connection closed
			if (dataBaseConnect.State == System.Data.ConnectionState.Closed)
			{
				try
				{
					//open then
					dataBaseConnect.Open();
				}
				catch
				{
					ErrorHandling("Cannot connect to Database");
				}
			}
		}
		public void CloseConnection()
		{
			//check is connection open
			if(dataBaseConnect.State == System.Data.ConnectionState.Open)
			{
				//close then
				dataBaseConnect.Close();
			}
		}

		//Load date from db
		public void LoadWebSitesDataFromDB()
		{
			LoadedWSList.Clear();//delete needless rows

			OpenConnection();

			//Select all rows from db
			sqlCommand = new SqlCommand(
				"SELECT * FROM dbo.WSCheck", dataBaseConnect);

			//exec sql querry
			sqlDataReader = sqlCommand.ExecuteReader();

			while(sqlDataReader.Read())
			{
				//Add new row from web site to list
				LoadedWSList.Add(new WebSite()
				{
					webSiteId = Convert.ToInt32(sqlDataReader["URLID"].ToString()),
					webSiteName = sqlDataReader["WSNAME"].ToString(),
					webSiteUrl = sqlDataReader["WSURL"].ToString(),
					requestTimeInterval = Convert.ToInt32(sqlDataReader["UPDTIME"].ToString()),
					workStatus = false,
				});
		}
			CloseConnection();
		}

		//submit list on demand
		public IEnumerable<WebSite> GetWebSiteList()
		{
			return LoadedWSList;
		}
		
		public void StopCheckingWebSites()
		{
			IsChekicngStarted = false;//thread IsAlive status switch to false and stop it
		}

		//start send requests to ws
		public void RunAvailabilityChecking()
		{
			//run checking 
			Task.Run(() =>
			{
				IsChekicngStarted = true;
				if (IsChekicngStarted)//cheching is not application closed
				Parallel.ForEach(LoadedWSList, currentWebSite =>//check as parallel
				{
					requestFromWS = WebRequest.Create(currentWebSite.webSiteUrl);//put url 
					requestFromWS.Timeout = currentWebSite.requestTimeInterval;//put time interval
					try
						{
						//send request await response
						webResponse = (HttpWebResponse)requestFromWS.GetResponse();
							if (webResponse.StatusCode == HttpStatusCode.OK)//code 200 - OK
							{
								//do we need to change status
								if (currentWebSite.workStatus == false)
								{
									currentWebSite.workStatus = true;//change status
									//call updating view
									UpdatingWebSiteStatus(LoadedWSList.IndexOf(currentWebSite),
									currentWebSite.webSiteName, currentWebSite.workStatus);
								}

							}
							else //code !=200 - not available
							{   //do we need to change status
								if (currentWebSite.workStatus == true)
								{
									currentWebSite.workStatus = false;//change status
									//call updating view
									UpdatingWebSiteStatus(currentWebSite.webSiteId,
									currentWebSite.webSiteName, currentWebSite.workStatus);
								}
							}
						}
						catch(Exception ex)
						{
							//no response due to wrong url, timeout, etc - set NA status
							if (currentWebSite.workStatus == true)
							{
								currentWebSite.workStatus = false;//change status
								//call updating view
								UpdatingWebSiteStatus(currentWebSite.webSiteId,
								currentWebSite.webSiteName, currentWebSite.workStatus);
							}
						ErrorHandling("Возможно, проблемы с соединением " +
							"или неправильно указаны данные.\n " +ex.Message + " \n" +
							currentWebSite.webSiteName + ": " + currentWebSite.webSiteUrl 
							+ "Время ожидания:" + currentWebSite.requestTimeInterval.ToString() + "мс");
						}
					});
			});
		}

		public void AddNewWebSiteDataToDB(string WebSiteName, 
			string WebSiteUrl, string timeinterval)
		{
			OpenConnection();

			//Insert new ws data to db
			try
			{
				sqlCommand = new SqlCommand(
				"INSERT INTO dbo.WSCheck VALUES ('" + WebSiteName + "', '" + WebSiteUrl + "', " +
				timeinterval + ")"
				, dataBaseConnect);

				sqlDataReader = sqlCommand.ExecuteReader();
			}
			catch(Exception ex)
			{
				ErrorHandling("Не удалось добавить данные." + ex.Message);
			}
			CloseConnection();

			//get website id
			sqlCommand = new SqlCommand("SELECT MAX(URLID) FROM dbo.WSCheck", dataBaseConnect);

			OpenConnection();
			
			sqlDataReader = sqlCommand.ExecuteReader();

			//add lo website list
			while (sqlDataReader.Read())
			{
				LoadedWSList.Add(new WebSite()
				{
					webSiteId = Convert.ToInt32(sqlDataReader[0].ToString()),
					webSiteName = WebSiteName,
					webSiteUrl = WebSiteUrl,
					requestTimeInterval = Convert.ToInt32(timeinterval),
					workStatus = false,
				});
			}

			CloseConnection();
		}

		//return website object with id = selected index
		public WebSite GetWebSiteBySelectedIndex(int selectedIndex)
		{
			return LoadedWSList[selectedIndex];
		}
		//remove data from db
		public void DeleteWebSiteFromDB(int webSiteID, int selectedIndex)
		{
			OpenConnection();
			try
			{
				//delete from db
				sqlCommand = new SqlCommand(
					"DELETE FROM dbo.WSCheck WHERE URLID =" +
					webSiteID.ToString(), dataBaseConnect);

				sqlDataReader = sqlCommand.ExecuteReader();
			}
			catch (Exception ex)
			{
				ErrorHandling("Не удалось удалить данные." + ex.Message);
			}


			CloseConnection();
		}
		
		//delete selected website from Loadedlist
		public void DeleteWebSiteFromList(int webSiteId, int selectedIndex)
		{
			LoadedWSList.RemoveAt(selectedIndex);
		}
		
		//Edit selected website data
		public void EditWebSiteToDB(int selectedIndex, string webSiteName,
			string webSiteUrl, string timeinterval)
		{
			OpenConnection();
			try
			{
				//Insert new ws data to db
				sqlCommand = new SqlCommand(
					"UPDATE dbo.WSCheck SET " +
					"WSNAME = '" + webSiteName + "', " +
					"WSURL = '" + webSiteUrl + "', " +
					"UPDTIME = " + timeinterval +
					"WHERE URLID = " + LoadedWSList[selectedIndex].webSiteId
					, dataBaseConnect);

				sqlDataReader = sqlCommand.ExecuteReader();
			}
			catch (Exception ex)
			{
				ErrorHandling("Не отредактировать данные." + ex.Message);
			}
			CloseConnection();
		}

		//edit data in list
		public void EditWebSiteList(int selectedIndex, string webSiteName,
			string webSiteUrl, string timeinterval)
		{
			LoadedWSList[selectedIndex].webSiteName = webSiteName;
			LoadedWSList[selectedIndex].webSiteUrl = webSiteUrl;
			LoadedWSList[selectedIndex].requestTimeInterval = 
				Convert.ToInt32(timeinterval);
		}
		
	}
}
