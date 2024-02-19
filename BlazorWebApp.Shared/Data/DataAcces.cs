using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BlazorWebApp.Shared.Data;


namespace BlazorWebApp.Shared.DataAccess
{
    public class DataAccess: IDataAccess
    {
        public HttpClient _client { get; set; }

        public List<Order> emplist { get; set; }

        public DataAccess(HttpClient client)
        {
            this._client = client;
        }

        public async Task<List<Order>> GetAllRecords()
        {
            await GetOrders();
            return emplist;
        }

        public async Task GetOrders()
        {
            emplist=await _client.GetFromJsonAsync<List<Order>>("api/Default");
          //  emplist = await _client.GetJsonAsync<Order[]>("api/Default");
        }
    }
}
