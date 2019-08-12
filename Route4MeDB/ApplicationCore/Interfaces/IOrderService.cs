using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using System.Threading.Tasks;

namespace Route4MeDB.ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order orderParameters);
    }
}
