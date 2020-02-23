using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Config
{
    public class Config
    {


        public T GetSetting<T>(string name)
        {
            return (T) Convert.ChangeType("aaa",typeof(T));
        }


        //GetSettings<Slots>(string slots)


        private string _file;

        public T Deserialize<T>(string file)
        {
            _file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file + ".od");

            if (File.Exists(_file))
            {
                using (FileStream stream = File.Open(_file, FileMode.Open))
                {
                    var item = (T)new BinaryFormatter().Deserialize(stream);

                    return item;
                }
            }
            return Activator.CreateInstance<T>();
        }
    }
}
