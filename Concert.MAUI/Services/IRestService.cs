namespace Concert.MAUI.Services
{
    public interface IRestService
    {
        Task<T?> GetAsync<T>(string endpoint);
        Task<T?> PostAsync<T>(string endpoint, object data);
        Task<T?> PutAsync<T>(string endpoint, object data);
        Task DeleteAsync(string endpoint);
    }
}
