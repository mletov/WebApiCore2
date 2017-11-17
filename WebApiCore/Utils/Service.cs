using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
//using System.Web.Script.Serialization;

namespace WebApiCore.Utils
{
    public class Service
    {

        public static IEnumerable<T> GetJsonFromApi<T>(string url)
        {
            try
            {
                string domain = "http://jsonplaceholder.typicode.com";
                url = domain + url;
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";

             
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                string responseText = "";
                using (var reader = new System.IO.StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                {
                    responseText += reader.ReadToEnd();
                }
            
                IEnumerable<T> json = JsonConvert.DeserializeObject<List<T>>(responseText);
                return json;
              
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static T GetJsonFromApiSingle<T>(string url)
        {
            string domain = "http://jsonplaceholder.typicode.com";
            url = domain + url;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";


            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            string responseText = "";
            using (var reader = new System.IO.StreamReader(resp.GetResponseStream(), Encoding.UTF8))
            {
                responseText += reader.ReadToEnd();
            }

            T json = JsonConvert.DeserializeObject<T>(responseText);
            return json;
        }
    }
}













//Для выхода с локалки юзаем прокси
/*
if (HttpContext.Current.Request.ServerVariables["HTTP_HOST"].Contains("localhost"))
{
    NetworkCredential nc = new NetworkCredential();
    nc.UserName = @"L1N1\LMA";
    nc.Password = "Mikel_789";
    req.Credentials = nc;
    IWebProxy proxy = new WebProxy("http://10.10.0.1:8080");
    proxy.Credentials = nc;
    req.Proxy = proxy;
}
*/
/*
JavaScriptSerializer js = new JavaScriptSerializer();
var objText = reader.ReadToEnd();
MyObject myojb = (MyObject)js.Deserialize(objText, typeof(MyObject));
*/
