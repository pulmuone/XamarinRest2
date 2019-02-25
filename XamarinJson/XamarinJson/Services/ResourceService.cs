using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinJson.Models;

namespace XamarinJson.Services
{
    public class ResourceService
    {
        HttpClient client;
        private static readonly ResourceService _instance = new ResourceService();
        public static string RestUrl = "http://172.28.200.58:8080";

        public List<Employee> Employees { get; private set; }
        private ResourceService()
        {
            var authData = string.Format("{0}:{1}", "admin", "1234");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public static ResourceService GetInstance()
        {
            return _instance;
        }

        public async Task<IList<T>> GetResources<T>()
        {
            var uri = new Uri(string.Format(RestUrl+"/{0}/all", typeof(T).Name.ToLower()));
            var content = string.Empty;
            try
            {
                var response = await client.GetAsync(uri);
                Debug.WriteLine(response.StatusCode.ToString());
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return new List<T>();
        }

        public async Task<T> GetResource<T>(long id)
        {
            var uri = new Uri(string.Format(RestUrl+"/{0}/{1}", typeof(T).Name.ToLower(), id));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return  JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return JsonConvert.DeserializeObject<T>(string.Empty);
        }

        public async Task DeleteResouce<T>(long empno)
        {
            var uri = new Uri(string.Format(RestUrl + "/{0}/{1}", typeof(T).Name.ToLower(), empno));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        public async Task SaveResouce<T>(T item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(RestUrl + "/{0}", typeof(T).Name.ToLower()));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }
    }
}   