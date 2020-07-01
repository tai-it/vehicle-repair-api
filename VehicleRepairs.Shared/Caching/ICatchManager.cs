namespace VehicleRepairs.Shared.Caching
{
    using System;
    using System.Threading.Tasks;

    public interface ICacheManager
    {
        T GetAndSet<T>(string key, int cacheSeconds, Func<T> func);

        void Remove(params string[] keys);

        Task<T> GetAndSetAsync<T>(object key, int cacheSeconds, Func<Task<T>> func);
    }
}
