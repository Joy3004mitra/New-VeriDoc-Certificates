using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;

using System.Threading;

using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace MyApp.db
{
    public static class VeriDocHttpHandler
    {

        /// <summary>
        /// Field is used to block to thread if another thread is creating HttpClient object.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// This field hold HttpClient object which is initialized in static property.
        /// </summary>
        private static volatile HttpClient httpClient;

        /// <summary>
        /// Gets the HttpClient object. 
        /// </summary>
        private static HttpClient Client
        {
            get
            {
                if (httpClient == null)
                {
                    lock (Locker)
                    {
                        httpClient = httpClient ?? new HttpClient();
                    }
                }

                return httpClient;
            }
        }

        /// <summary>
        /// Method is used to process incoming Get API request asynchronously and return generic result.
        /// </summary>
        /// <typeparam name="T">Generic type provided by consumer class.</typeparam>
        /// <param name="url">API Path</param>
        /// <param name="timeOut">Request time out value</param>
        /// <returns>Generic Task result</returns>
        public static async Task<T> Get<T>(Uri url, int timeOut)
        {
            T value;
            try
            {

                value = await GetHandler<T>(url, timeOut).ConfigureAwait(false);
                return value;

            }
            catch(Exception e)
            {
            
                return (T)Convert.ChangeType(0, typeof(T));
            }

               
            
           

        }

        /// <summary>
        /// Method is used to process incoming Get API request asynchronously and return generic result. 
        /// </summary>
        /// <typeparam name="T">Generic type provided by consumer class.</typeparam>
        /// <param name="url">API Path</param>
        /// <param name="timeOut">Request time out value</param>
        /// <returns>Generic Task result</returns>
        private static async Task<T> GetHandler<T>(Uri url, int timeOut)
        {
            T value = default(T);
            using (CancellationTokenSource cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(timeOut)))
            {
                using (HttpResponseMessage response = await Client.GetAsync(url, cancellationToken.Token).ConfigureAwait(false))
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        value = JsonConvert.DeserializeObject<T>(content);
                    }
                    else
                    {
                        throw new HttpRequestException($"{response.StatusCode}:{content}");
                    }

                    return value;
                }
            }
        }

        /// <summary>
        /// Method is used to process incoming Get API request asynchronously and return generic result.
        /// </summary>
        /// <typeparam name="T">Generic type provided by consumer class.</typeparam>
        /// <param name="url">API Path</param>
        /// <param name="timeOut">Request time out value</param>
        /// <param name="apiKey">api key</param>
        /// <param name="payload">payload value</param>
        /// <returns>Generic Task result</returns>
        public static async Task<T> Get<T>(Uri url, int timeOut, string apiKey, string payload)
        {
            T value = await GetHandlerWithHeader<T>(url, timeOut, apiKey, payload).ConfigureAwait(false);
            return value;
        }
        /// <summary>
        /// Method is used to process incoming Get API request asynchronously and return generic result. 
        /// </summary>
        /// <typeparam name="T">Generic type provided by consumer class.</typeparam>
        /// <param name="url">API Path</param>
        /// <param name="timeOut">Request time out value</param>
        /// <param name="apiKey">api key</param>
        /// <param name="payload">payload value</param>
        /// <returns>Generic Task result</returns>
        private static async Task<T> GetHandlerWithHeader<T>(Uri url, int timeOut, string apiKey, string payload)
        {
            T value = default(T);
            using (CancellationTokenSource cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(timeOut)))
            {
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Add("apikey", apiKey);
                Client.DefaultRequestHeaders.Add("payload", payload);
                using (HttpResponseMessage response = await Client.GetAsync(url, cancellationToken.Token).ConfigureAwait(false))
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        value = JsonConvert.DeserializeObject<T>(content);
                    }
                    else
                    {
                        throw new HttpRequestException($"{response.StatusCode}:{content}");
                    }

                    return value;
                }
            }
        }

        /// <summary>
        ///  Method is used to Post json data asynchronously and return string result.
        /// </summary>
        /// <typeparam name="T">Generic type provided by consumer class.</typeparam>
        /// <param name="url">API Path</param>
        /// <param name="timeOut">Request time out value</param>
        /// <param name="apiKey">api key</param>
        /// <param name="payload">payload value</param>
        /// <returns>string result</returns>
        public static async Task<T> Post<T>(Uri url, int timeOut, string apiKey, string payload)
        {
            return await PostHandlerWithHeader<T>(url, timeOut, apiKey, payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Method is used to Post json data asynchronously and return string result.
        /// </summary>
        /// <typeparam name="T">Generic type provided by consumer class.</typeparam>
        /// <param name="url">API Path</param>
        /// <param name="timeOut">Request time out value</param>
        /// <param name="apiKey">api key</param>
        /// <param name="payload">payload value</param>
        /// <returns>string result</returns>
        private static async Task<T> PostHandlerWithHeader<T>(Uri url, int timeOut, string apiKey, string payload)
        {
            T value = default(T);
            using (CancellationTokenSource cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(timeOut)))
            {
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Add("apikey", apiKey);
                Client.DefaultRequestHeaders.Add("payload", payload);
                using (HttpResponseMessage response = await Client.PostAsync(url, new StringContent(new JavaScriptSerializer().Serialize("[]"), Encoding.UTF8, "application/json"), cancellationToken.Token).ConfigureAwait(false))
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        value = JsonConvert.DeserializeObject<T>(content);
                    }
                    else
                    {
                        throw new HttpRequestException($"{response.StatusCode}:{content}");
                    }
                    response.Dispose();
                }
            }
            return value;
        }

        /// <summary>
        ///  Method is used to Post json data asynchronously and return string result.
        /// </summary>
        /// <typeparam name="T">Generic type provided by consumer class.</typeparam>
        /// <param name="url">API Path</param>
        /// <param name="parameter">Data to submit to server in post request, generally a model object</param>
        /// <returns>string result</returns>
        public static async Task<string> Post<T>(Uri url, int timeOut, string apiKey, string payload, T parameter)
        {
            return await PostHandler<T>(url, timeOut, apiKey, payload, parameter).ConfigureAwait(false);
        }

        /// <summary>
        /// Method is used to Post json data asynchronously and return string result.
        /// </summary>
        /// <typeparam name="T">Generic type provided by consumer class.</typeparam>
        /// <param name="url">API Path</param>
        /// <param name="timeOut">Request time out value</param>
        /// <param name="parames">Data to submit to server in post request, generally a model object</param>
        /// <returns>string result</returns>
        private static async Task<string> PostHandler<T>(Uri url, int timeOut, string apiKey, string payload, T parames)
        {
            using (CancellationTokenSource cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(timeOut)))
            {
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Add("apikey", apiKey);
                Client.DefaultRequestHeaders.Add("payload", payload);
                using (HttpResponseMessage response = await Client.PostAsync(url, new StringContent(new JavaScriptSerializer().Serialize(parames), Encoding.UTF8, "application/json"), cancellationToken.Token).ConfigureAwait(false))
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        return content;
                    }
                    else
                    {
                        throw new HttpRequestException($"{response.StatusCode}:{content}");
                    }
                }
            }
        }
    }
}
