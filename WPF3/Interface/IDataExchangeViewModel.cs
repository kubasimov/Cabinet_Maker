using System;
using WPF3.Enum;

namespace WPF3.Interface
{
    public interface IDataExchangeViewModel
    {
        Object Item(EnumExchangeViewmodel name);

        bool ContainsKey(EnumExchangeViewmodel name);

        void Add(EnumExchangeViewmodel key, object value);

        void Delete(EnumExchangeViewmodel key);
    }
}