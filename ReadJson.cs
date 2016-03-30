using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

public partial class ReadJson : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		string json = get_web_content("http://4rapiddev.com/demo/Data/devices.json");

		dynamic array = JsonConvert.DeserializeObject(json);

		Response.Write("Code: " + array.Code + "<br>");
		Response.Write("Message: " + array.Message + "<br>");

		dynamic Data = array.Data;

		Response.Write("We have : " + Data.Index + " devices<br>");

		dynamic DeviceList = Data.Devices;

		Response.Write("<ul>");

		foreach (var item in DeviceList)
		{
			Response.Write("<li>");

			Response.Write("PushToken: " + item["PushToken"] + "<br>");
			Response.Write("DeviceId: " + item["DeviceId"] + "<br>");
			Response.Write("DeviceType: " + item["DeviceType"] + "<br>");

			Response.Write("</li>");
		}
		Response.Write("</ul>");
	}

	public string get_web_content(string url)
	{
		Uri uri = new Uri(url);
		HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
		request.Method = WebRequestMethods.Http.Get;
		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream());
		string output = reader.ReadToEnd();
		response.Close();

		return output;
	}
}