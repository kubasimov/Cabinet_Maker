using Config.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Config.Sync
{
    interface ISynchrontization
    {
        void BaseSave(BaseConfiguration baseConfiguration);
        BaseConfiguration BaseRead();
        CabinetModel CabRead();
        void CabSave(CabinetModel cabConf);
    }
}
