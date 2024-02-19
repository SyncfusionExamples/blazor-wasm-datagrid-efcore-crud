using BlazorWebApp.Shared.Data;
using BlazorWebApp.Shared.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorWebApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private OrdersDetailsContext _context;
        OrderDataAccessLayer db=new OrderDataAccessLayer();
        public OrdersController ( OrdersDetailsContext context )
        {
            _context = context;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public object Get ()
        {
            IQueryable<Order> data=db.GetAllOrders().AsQueryable();
            var count=data.Count();
            var queryString = Request.Query;
            string sort = queryString["$orderby"];
            string filter = queryString["$filter"];
            if (sort != null) //Sorting
            {
                var sortfield = sort.Split(' ');
                var sortColumn = sortfield[0];
                if (sortfield.Length == 2)
                {
                    sortColumn = sortfield[0];
                    switch (sortColumn)
                    {
                        case "OrderDate":
                            data = data.OrderByDescending(x => x.OrderDate);
                            break;
                        case "CustomerId":
                            data = data.OrderByDescending(x => x.CustomerId);
                            break;
                        case "Freight":
                            data = data.OrderByDescending(x => x.Freight);
                            break;
                    }
                }
                else
                {
                    switch (sortColumn)
                    {
                        case "OrderDate":
                            data = data.OrderBy(x => x.OrderDate);
                            break;
                        case "CustomerId":
                            data = data.OrderBy(x => x.CustomerId);
                            break;
                        case "Freight":
                            data = data.OrderBy(x => x.Freight);
                            break;
                    }
                }
            }
            if (filter != null)
            {
                var newfiltersplits = filter;
                var filtersplits=newfiltersplits.Split('(', ')', ' ');
                var filterfield = filtersplits[1];
                var filtervalue = filtersplits[3];
                if (filtersplits.Length == 7)
                {
                    if (filtersplits[2] =="tolower")
                    {
                        filterfield = filter.Split('(', ')', '\'')[3];
                        filtervalue = filter.Split('(', ')', '\'')[5];
                    }
                }
                switch (filterfield)
                {
                    case "OrderDate":

                        data = (from cust in data
                                where cust.OrderDate.ToString() == filtervalue.ToString()
                                select cust);
                        break;
                    case "CustomerId":
                        data = (from cust in data
                                where cust.CustomerId.ToString() == filtervalue.ToString()
                                select cust);
                        break;
                    case "Freight":
                        data = (from cust in data
                                where cust.Freight.ToString() == filtervalue.ToString()
                                select cust);
                        break;

                }
            }
            if (queryString.Keys.Contains("$inlinecount"))
            {
                StringValues Skip;
                StringValues Take;
                int skip = (queryString.TryGetValue("$skip", out Skip)) ? Convert.ToInt32(Skip[0]) : 0;
                int top = (queryString.TryGetValue("$top", out Take)) ? Convert.ToInt32(Take[0]) : data.Count();
                return new { Items = data.Skip(skip).Take(top), Count = count };
            }
            else
            {
                return data;
            }
            //return new { Items = _context.Orders, Count = _context.Orders.Count() };
        }
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        // POST api/<OrdersController>
        [HttpPost]
        public void Post ( [FromBody] Order book )
        {
            _context.Orders.Add(book);
            _context.SaveChanges();
        }
        // PUT api/<OrdersController>
        [HttpPut]
        public void Put ( long id, [FromBody] Order book )
        {
            Order _book = _context.Orders.Where(x => x.OrderId.Equals(book.OrderId)).FirstOrDefault();
            _book.CustomerId = book.CustomerId;
            _book.Freight = book.Freight;
            _book.OrderDate = book.OrderDate;
            _context.SaveChanges();
        }
        // DELETE api/<OrdersController>
        [HttpDelete("{id}")]
        public void Delete ( long id )
        {
            Order _book = _context.Orders.Where(x => x.OrderId.Equals(id)).FirstOrDefault();
            _context.Orders.Remove(_book);
            _context.SaveChanges();
        }
    }
}
