using CoreS.Config.ConfigModel;
using CoreS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreS.Config
{
    public class Settings
    {
        public SlotsConfigModel GetSlots()
        {
            return new SlotsConfigModel();
        }
        
        public 
        
        
        
        
        
        
        
        
        Settings()
        { }
        private static readonly object padlock = new object();
        private static Settings instance = null;
        public static Settings Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance==null)
                    {
                        instance = new Settings();

                    }
                    return instance;
                }
            }
        }
    }
}
