using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorWebApp.Shared.Data;

namespace BlazorWebApp.Shared.DataAccess
{
    public interface IDataAccess
    {
        Task<List<Order>> GetAllRecords();
    }
}
