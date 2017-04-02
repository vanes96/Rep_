using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository
    {
        IEnumerable<Order> GetJournal();
        IEnumerable<User> GetClients();
        IEnumerable<Driver> GetDrivers();
        IEnumerable<Order> GetHistoryClient(int clientID);
        IEnumerable<Order> GetHistoryDriver(int driverID);
        Car GetCar(string number);
        Driver GetDriver(int id);
        User GetUser(int id);
        Order GetOrder(int id);
        Status GetStatus(int id);
        Order GetOrderDriver(int driverID);
        Order GetOrderClient(int clientID);

        Order CreateOrder(Order order, int clientID);
        Driver CreateDriver(Driver driver);

        void UpdateDriver(Driver driver);
        void UpdateUser(User user);
        void AcceptOrder(Order order, int driverID);
        void PrepareOrder(int orderID);
        void StartOrder(int orderID);
        void FinishOrder(int orderID);
        void CancelOrder(int orderID, User user);

        void DeleteDriver(int id);
        void DeleteClient(int id);

        void ReadAll();
        void SaveJournal();
        void SaveUsers();
        void SaveDrivers();
        void SaveCars();
        void SaveStatuses();
        
    }
}
