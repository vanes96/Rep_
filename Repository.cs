using DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace DataAccess
{
    public class Repository : IRepository
    {
        private static Repository rep = new Repository();
        public static Repository Current
        {
            get { return rep; }
        }
        public Repository()
        {
            ReadAll();
        }

        private List<Order> Journal { get; set; }
        private List<User> Users { get; set; }
        private List<Driver> Drivers { get; set; }
        private List<Car> Cars { get; set; }
        private List<Status> Statuses { get; set; }

        //---------------------- GET ------------------------
        //public IEnumerable<User> GetUsers()
        //{
        //    return Users;
        //}
        public IEnumerable<Order> GetJournal()
        {
            return Journal;
        }
        public IEnumerable<User> GetClients()
        {
            List<User> clients = new List<User>();
            foreach (User user in Users)
                if (!user.IsAdmin)
                    clients.Add(user);
            return clients;
        }
        public IEnumerable<Driver> GetDrivers()
        {
            return Drivers;
        }
        public IEnumerable<Order> GetHistoryClient(int clientID)
        {
            List<Order> history = new List<Order>();
            foreach (Order order in Journal)
                if (order.ClientID == clientID)
                    history.Add(order);
            return history;
        }
        public IEnumerable<Order> GetHistoryDriver(int driverID)
        {
            List<Order> history = new List<Order>();
            foreach (Order order in Journal)
                if (order.DriverID == driverID)
                    history.Add(order);
            return history;
        }
        public Car GetCar(string number)
        {
            return Cars.Single(c => c.Number == number);
        }
        public Driver GetDriver(int id)
        {
            return Drivers.Single(d => d.ID == id);
        }
        public User GetUser(int id)
        {
            return Users.Single(u => u.ID == id);
        }
        public Order GetOrder(int id)
        {
            return Journal.Single(o => o.ID == id);
        }
        public Status GetStatus(int id)
        {
            return Statuses.Single(s => s.ID == id);
        }
        public Order GetOrderDriver(int driverID)
        {
            return Journal.Single(o => o.DriverID == driverID);
        }
        public Order GetOrderClient(int clientID)
        {
            return Journal.Single(o => o.ClientID == clientID);
        }
        //---------------------------------------------------
        //---------------------- CREATE ---------------------
        public Order CreateOrder(Order order, int clientID)
        {
            order.ID = Journal.Count;
            order.ClientID = clientID;
            order.StatusID = Statuses.Single(s => s.Name == "ожидание принятия").ID;
            order.TimeCall = DateTime.Now;
            Journal.Add(order);

            SaveJournal();
            return order;
        }
        public Driver CreateDriver(Driver driver)
        {
            driver.ID = Drivers.Count;
            Drivers.Add(driver);
            SaveDrivers();
            return driver;
        }
        //---------------------------------------------------
        //---------------------- DELETE ---------------------
        public void DeleteDriver(int id)
        {
            Drivers.Remove(Drivers.Single(d => d.ID == id));
            SaveDrivers();
        }
        public void DeleteClient(int id)
        {
            Users.Remove(Users.Single(u => u.ID == id));
            SaveUsers();
        }
        //---------------------------------------------------
        //---------------------- UPDATE ---------------------
        public void UpdateDriver(Driver driver)
        {
            Drivers[Drivers.IndexOf(Drivers.Single(d => d.ID == driver.ID))] = driver;
            SaveDrivers();
        }
        public void UpdateUser(User user)
        {
            Users[Users.IndexOf(Users.Single(u => u.ID == user.ID))] = user;
            SaveUsers();
        }
        //----------
        public void AcceptOrder(Order order, int driverID)
        {
            Journal[Journal.IndexOf(Journal.Single(o => o.ID == order.ID))].Price = order.Price;
            Journal[Journal.IndexOf(Journal.Single(o => o.ID == order.ID))].DriverID = driverID;
            Journal[Journal.IndexOf(Journal.Single(o => o.ID == order.ID))].StatusID = Statuses.Single(s => s.Name == "ожидание водителя").ID;
            SaveJournal();
        }
        public void PrepareOrder(int orderID)
        {
            //Journal[Journal.IndexOf(Journal.Single(o => o.ID == orderID))].TimeStart = DateTime.Now;
            Journal[Journal.IndexOf(Journal.Single(o => o.ID == orderID))].StatusID = Statuses.Single(s => s.Name == "ожидание клиента").ID;
            SaveJournal();
        }
        public void StartOrder(int orderID)
        {
            Journal[Journal.IndexOf(Journal.Single(o => o.ID == orderID))].TimeStart = DateTime.Now;
            Journal[Journal.IndexOf(Journal.Single(o => o.ID == orderID))].StatusID = Statuses.Single(s => s.Name == "поездка").ID;
            SaveJournal();
        }
        public void FinishOrder(int orderID)
        {
            Journal[Journal.IndexOf(Journal.Single(o => o.ID == orderID))].TimeFinish = DateTime.Now;
            Journal[Journal.IndexOf(Journal.Single(o => o.ID == orderID))].StatusID = Statuses.Single(s => s.Name == "завершение").ID;
            SaveJournal();
        }
        public void CancelOrder(int orderID, User user)
        {
            if (user is Driver)
                Journal[Journal.IndexOf(Journal.Single(o => o.ID == orderID))].StatusID = Statuses.Single(s => s.Name == "отмена водителем").ID;
            else
                Journal[Journal.IndexOf(Journal.Single(o => o.ID == orderID))].StatusID = Statuses.Single(s => s.Name == "отмена клиентом").ID;
            SaveJournal();
        }
        //---------------------------------------------------
        //===================================================
        //----------------------- JSON ----------------------
        private void ReadAll()
        {
            StreamReader Reader; 
            Reader = new StreamReader(HostingEnvironment.MapPath("~/Journal.json"));
            Journal = JsonConvert.DeserializeObject<List<Order>>(Reader.ReadToEnd());

            Reader = new StreamReader(HostingEnvironment.MapPath("~/Users.json"));
            Users = JsonConvert.DeserializeObject<List<User>>(Reader.ReadToEnd());

            Reader = new StreamReader(HostingEnvironment.MapPath("~/Drivers.json"));
            Drivers = JsonConvert.DeserializeObject<List<Driver>>(Reader.ReadToEnd());

            Reader = new StreamReader(HostingEnvironment.MapPath("~/Cars.json"));
            Cars = JsonConvert.DeserializeObject<List<Car>>(Reader.ReadToEnd());

            Reader = new StreamReader(HostingEnvironment.MapPath("~/Statuses.json"));
            Statuses = JsonConvert.DeserializeObject<List<Status>>(Reader.ReadToEnd());

            Reader.Close();
        }
        private void SaveJournal()
        {
            StreamWriter Writer = new StreamWriter(HostingEnvironment.MapPath("~/Journal.json"));
            string output = JsonConvert.SerializeObject(Journal, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd hh:mm:ss" });
            Writer.Write(output);
            Writer.Close();
        }
        private void SaveUsers()
        {
            StreamWriter Writer = new StreamWriter(HostingEnvironment.MapPath("~/Users.json"));
            string output = JsonConvert.SerializeObject(Users);
            Writer.Write(output);
            Writer.Close();
        }
        private void SaveDrivers()
        {
            StreamWriter Writer = new StreamWriter(HostingEnvironment.MapPath("~/Drivers.json"));
            string output = JsonConvert.SerializeObject(Drivers);
            Writer.Write(output);
            Writer.Close();
        }
        private void SaveCars()
        {
            StreamWriter Writer = new StreamWriter(HostingEnvironment.MapPath("~/Cars.json"));
            string output = JsonConvert.SerializeObject(Cars);
            Writer.Write(output);
            Writer.Close();
        }
        private void SaveStatuses()
        {
            StreamWriter Writer = new StreamWriter(HostingEnvironment.MapPath("~/Statuses.json"));
            string output = JsonConvert.SerializeObject(Statuses);
            Writer.Write(output);
            Writer.Close();
        }
        //---------------------------------------------------
    }
}
