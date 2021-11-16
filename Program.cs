using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSC
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Model webSiteModel = new Model();

			mainView view = new mainView();

			mainPresentor presentor = new mainPresentor(webSiteModel, view);

			Application.Run(view);
		}
	}
}
