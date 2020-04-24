using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fridgr.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetMyItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetItemsAsync(int increment = 20, int reloads = 0, bool forceRefresh = false);
    }
}
