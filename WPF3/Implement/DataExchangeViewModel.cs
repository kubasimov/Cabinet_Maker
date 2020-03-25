using NLog;
using System.Collections.Generic;
using WPF3.Enum;
using WPF3.Interface;

namespace WPF3.Implement
{
    public class DataExchangeViewModel:IDataExchangeViewModel
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<EnumExchangeViewmodel, object> _dictionary =
            new Dictionary<EnumExchangeViewmodel, object>();

        private static DataExchangeViewModel _instance;

        public static DataExchangeViewModel Instance
        {
            get { return _instance ?? (_instance = new DataExchangeViewModel()); }
        }

        public object Item(EnumExchangeViewmodel name)
        {
            Logger.Info("Item(EnumExchangeViewmodel name)");
            Logger.Debug("name: {0}", name);
            object item = _dictionary[name];
            Delete(name);
            return item;
        }

        public bool ContainsKey(EnumExchangeViewmodel name)
        {
            Logger.Info("ContainsKey(EnumExchangeViewmodel name)");
            Logger.Debug("name: {0}", name);
            return _dictionary.ContainsKey(name);
        }

        public void Add(EnumExchangeViewmodel key, object value)
        {
            Logger.Info("Add(EnumExchangeViewmodel key, object value)");
            Logger.Debug("key: {0}, value: {1}", key, value);
            if (ContainsKey(key))
            {
                Delete(key);
            }
            _dictionary.Add(key, value);
        }

        public void Delete(EnumExchangeViewmodel key)
        {
            Logger.Info("Delete(EnumExchangeViewmodel key)");
            Logger.Debug("key: {0}", key);
            _dictionary.Remove(key);
        }
    }
}