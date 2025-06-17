namespace Services
{
    public interface IServiceLocator
    {
        void RegisterService<T>(T service);
        bool GetService<T>(out T service);
        T GetService<T>();
        bool HasService<T>();
    }
}