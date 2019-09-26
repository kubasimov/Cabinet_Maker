using System.Collections.Generic;
using WPF3.Enum;
using WPF3.Interface;

namespace WPF3.Implement
{
    public class DataExchangeViewModel:IDataExchangeViewModel
    {
        private readonly Dictionary<EnumExchangeViewmodel, object> _dictionary =
            new Dictionary<EnumExchangeViewmodel, object>();

        private static DataExchangeViewModel _instance;

        public static DataExchangeViewModel Instance
        {
            get { return _instance ?? (_instance = new DataExchangeViewModel()); }
        }

        public object Item(EnumExchangeViewmodel name)
        {
            object item = _dictionary[name];
            Delete(name);
            return item;
        }

        public bool ContainsKey(EnumExchangeViewmodel name)
        {
            return _dictionary.ContainsKey(name);
        }

        public void Add(EnumExchangeViewmodel key, object value)
        {
            if (ContainsKey(key))
            {
                Delete(key);
            }
            _dictionary.Add(key, value);
        }

        public void Delete(EnumExchangeViewmodel key)
        {
            _dictionary.Remove(key);
        }
    }
}