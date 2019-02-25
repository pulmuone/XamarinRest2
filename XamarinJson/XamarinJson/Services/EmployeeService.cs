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
    public class EmployeeService
    {
        HttpClient client;
        private static readonly EmployeeService _instance = new EmployeeService();
        public static string RestUrl = "http://172.28.200.58:8080/emp";

        public List<Employee> Employees { get; private set; }
        private EmployeeService()
        {
            var authData = string.Format("{0}:{1}", "admin", "1234");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public static EmployeeService GetInstance()
        {
            return _instance;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            Employees = new List<Employee>();

            var uri = new Uri(string.Format(RestUrl+"/all", string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Employees = JsonConvert.DeserializeObject<List<Employee>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Employees;
        }

        public async Task<Employee> GetEmployee(long empno)
        {
            Employee employee = new Employee();

            var uri = new Uri(string.Format(RestUrl+"/{0}", empno));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<Employee>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return employee;
        }

        public async Task DeleteEmployee(long empno)
        {
            var uri = new Uri(string.Format(RestUrl + "/{0}", empno));

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

        public async Task SaveEmployee(Employee employee, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(employee);
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
   