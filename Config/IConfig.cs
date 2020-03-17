namespace Config
{
    public interface IConfig
    {
        //public T GetSetting<T>(string name);
        public T GetSlots<T>();
    }
}