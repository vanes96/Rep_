using DataAccess;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaxiT.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        private Repository Rep = Repository.Current;
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        public Car GetCar(string number)
        {
            return Rep.GetCar(number);
        }
        public Driver GetDriver(int id)
        {
            return Rep.GetDriver(id);
        }
        public User GetUser(int id)
        {
            return Rep.GetUser(id);
        }
        public Order GetOrder(int id)
        {
            return Rep.GetOrder(id);
        }
        public Status GetStatus(int id)
        {
            return Rep.GetStatus(id);
        }
        public Order GetOrderDriver(int driverID)
        {
            return Rep.GetOrderDriver(driverID);
        }
        public Order GetOrderClient(int clientID)
        {
            return Rep.GetOrderClient(clientID);
        }
        public IEnumerable<Order> GetJournal()
        {
            return Rep.GetJournal();
        }
        public IEnumerable<User> GetClients()
        {
            return Rep.GetClients();
        }
        public IEnumerable<Driver> GetDrivers()
        {
            return Rep.GetDrivers();
        }
        public IEnumerable<Order> GetHistoryClient(int clientID)
        {
            return Rep.GetHistoryClient(clientID);
        }
        public IEnumerable<Order> GetHistoryDriver(int driverID)
        {
            return Rep.GetHistoryDriver(driverID);
        }

        [HttpPost]
        public Order CreateOrder(Order order, int clientID)
        {
            return Rep.CreateOrder(order, clientID);
        }
        [HttpPost]
        public Driver CreateDriver(Driver driver)
        {
            return Rep.CreateDriver(driver);
        }


        [HttpPut]
        public void UpdateDriver(Driver driver)
        {
            Rep.UpdateDriver(driver);
        }
        [HttpPut]
        public void UpdateUser(User user)
        {
            Rep.UpdateUser(user);
        }
        [HttpPut]
        public void AcceptOrder(Order order, int driverID)
        {
            Rep.AcceptOrder(order, driverID);
        }
        [HttpPut]
        public void PrepareOrder(int orderID)
        {
            Rep.PrepareOrder(orderID);
        }
        [HttpPut]
        public void StartOrder(int orderID)
        {
            Rep.StartOrder(orderID);
        }
        [HttpPut]
        public void FinishOrder(int orderID)
        {
            Rep.FinishOrder(orderID);
        }
        [HttpPut]
        public void CancelOrder(int orderID, User user)
        {
            Rep.CancelOrder(orderID, user);
        }


        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void DeleteClient(int id)
        {
            Rep.DeleteClient(id);
        }
        public void DeleteDriver(int id)
        {
            Rep.DeleteDriver(id);
        }
    }
}
