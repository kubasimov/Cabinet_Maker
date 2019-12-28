using System;
using System.Collections.Generic;
using System.Text;

namespace Config
{
    public interface IConfig
    {
        public T GetSetting<T>() where T : class, new();
    }
}
