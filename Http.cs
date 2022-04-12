using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfApp1
{
    public static class Http
    {
        public static string? HttpPost(string url, string param, HttpClient httpClient)
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            HttpContent httpContent = new StringContent(param);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
            {
                CharSet = "utf-8"
            };
            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }
        public static string? HttpGet(string url, bool bol, HttpClient httpClient)
        {
            try
            {
                Task<string> request = httpClient.GetStringAsync(new Uri(url));
                if (bol)
                {
                    RootObject? rb = JsonConvert.DeserializeObject<RootObject>(request.Result);
                    return rb.Status switch
                    {
                        -1 => rb.Msg,
                        1 => rb.Msg,
                        _ => null
                    };
                }
                else
                {
                    return request.Result;
                }
            }
            catch
            {
                return null;
            }
        }

    }
    public class HTTPSa
    {
        public static async void Getback(string back, string filepath, int len, HttpClient httpClient)
        {
            using FileStream fs = new(filepath, FileMode.Truncate, FileAccess.ReadWrite);
            var buffer = new byte[0x1000];
            var bytes = await httpClient.GetStreamAsync(Init.IP + Init.Filedrv + back).ConfigureAwait(false);
            int down;
            float cent = 0;
            long totalRead = 0;
            while ((down = await bytes.ReadAsync(buffer)) > 0)
            {
                await fs.WriteAsync(buffer.AsMemory(0, down));
                totalRead += down;
                cent = (float)totalRead / len * 100;
               // Ui_send(cent);
                Init._form.progressBar1.Value = (int)cent;
                Init._form.resultLabel.Content = Init.Numc + " 下载进度:" + cent.ToString("#0.00") + "%";
            }
            Init.Numc--;
            MainWindow.Pd(true);
        }
    }
}
