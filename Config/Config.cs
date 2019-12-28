namespace Config
{
    public class Config : IConfig
    {
        public T GetSetting<T>() where T : class, new() 
        {
            return new T();
        }
    }
}
