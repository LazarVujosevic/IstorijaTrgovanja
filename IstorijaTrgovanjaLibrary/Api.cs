using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IstorijaTrgovanjaLibrary
{
    public class Api
    {
        public Task<List<string>> CallApi(string sifra)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            CancellationTokenSource source = new CancellationTokenSource();

            if (!string.IsNullOrEmpty(sifra))
            {
                try
                {
                    return Task.Run(() => this.GetList(sifra));

                }
                catch (Exception ex)
                {
                    return Task.Run(() => new List<string>());
                }
            }
            else
            {
                return Task.Run(() => new List<string>());
            }
        }

        public List<string> GetList(string sifra)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create("h" + $"ttps://webservice.gvsi.com/query/csv/GetDaily/tradedatetimegmt/open/high/low/close/volume?pricesymbol=%22{sifra}%22&daysBack=100");
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "text/csv; encoding='utf-8'";
                httpWebRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("desktopapp:ThePa55word"));

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string results = sr.ReadToEnd();
                    var listOfRecords = results.Split(new string[] { "\n" }, StringSplitOptions.None).Select(x => x.Replace("\r", "")).ToList();
                    sr.Close();
                    var disposableResponse = httpWebResponse as IDisposable;
                    disposableResponse.Dispose();
                    return listOfRecords;
                }

            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public void CancelTask()
        {
            
        }
    }
}
