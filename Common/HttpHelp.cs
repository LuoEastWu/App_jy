using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpHelp
    {
        /// <summary>
        /// 发送基于HttpClient的Post发布请求
        /// </summary>
        /// <param name="requestUrl">你的post网址</param>
        /// <param name="routeParameters">你的post参数</param>
        /// <returns>返回一个响应对象</returns>
        public static async Task<String> SendPostRequestBasedOnHttpClient(string requestUrl, IDictionary<string, string> routeParameters)
        {
            string returnValue = string.Empty;
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            Uri uri = new Uri(requestUrl);
            var content = new FormUrlEncodedContent(routeParameters);
            try
            {
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    returnValue = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

        /// <summary>
        /// 基于HttpClient发送get获取请求
        /// </summary>
        /// <param name="requestUrl">你的请求地址</param>
        /// <param name="routeParameters">请求参数</param>
        /// <returns>返回一个响应对象</returns>
        public static async Task<String> SendGetRequestBasedOnHttpClient(string requestUrl, IDictionary<string, string> routeParameters)
        {
            string stringValue = string.Empty;
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            //format the url paramters
            string paramters = string.Join("&", routeParameters.Select(p => p.Key + "=" + p.Value));
            Uri uri = new Uri(string.Format("{0}?{1}", requestUrl, paramters));
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    stringValue = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stringValue;
        }

        /// <summary>
        /// send the get request based on HttpWebRequest
        /// </summary>
        /// <param name="requestUrl">the url you post</param>
        /// <param name="routeParameters">the parameters you post</param>
        /// <returns>return a response object</returns>
        public static async Task<String> SendGetHttpRequestBaseOnHttpWebRequest(string requestUrl, IDictionary<string, string> routeParameters)
        {
            string stringValue = string.Empty;
            string paramters = string.Join("&", routeParameters.Select(p => p.Key + "=" + p.Value));
            Uri uri = new Uri(string.Format("{0}?{1}", requestUrl, paramters));
            var request = (HttpWebRequest)HttpWebRequest.Create(uri);

            using (var response = request.GetResponseAsync().Result as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            stringValue = await reader.ReadToEndAsync();
                        }
                    }
                }
            }
            return stringValue;
        }

        /// <summary>
        /// send the post request based on httpwebrequest
        /// </summary>
        /// <param name="url">the url you post</param>
        /// <param name="routeParameters">the parameters you post</param>
        /// <returns>return a response object</returns>
        public static async Task<String> SendPostHttpRequestBaseOnHttpWebRequest(string url, IDictionary<string, string> routeParameters)
        {
            string stringValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";

            byte[] postBytes = null;
            request.ContentType = "application/x-www-form-urlencoded";
            string paramters = string.Join("&", routeParameters.Select(p => p.Key + "=" + p.Value));
            postBytes = Encoding.UTF8.GetBytes(paramters.ToString());

            using (Stream outstream = request.GetRequestStreamAsync().Result)
            {
                outstream.Write(postBytes, 0, postBytes.Length);
            }
            using (HttpWebResponse response = request.GetResponseAsync().Result as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            stringValue = await reader.ReadToEndAsync();
                        }
                    }
                }
            }
            return stringValue;
        }


        /// <summary>  
        /// GET请求与获取结果  
        /// </summary>  
        public static async Task<string> HttpGet(string Url, string postDataStr="", string ContentType = "text/html;charset=UTF-8")
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.Timeout = 3000;
                request.ContentType = ContentType;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = await myStreamReader.ReadToEndAsync();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 通过Post方式发送并返回Http结果
        /// </summary>
        /// <param name="Url">Url地址</param>
        /// <param name="Code">编码格式</param>
        /// <param name="postData">参数型POST数据</param>
        /// <returns></returns>
        public static String HttpPost(string Url, Encoding Code, NameValueCollection postData)
        {
            string returns = "";
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    // 向服务器发送POST数据
                    byte[] responseArray = webClient.UploadValues(Url, postData);
                    string response = Code.GetString(responseArray);
                    returns = response;
                }
            }
            catch (Exception e)
            {
                returns = (e.Message);
            }
            return returns;
        }

    }
}
