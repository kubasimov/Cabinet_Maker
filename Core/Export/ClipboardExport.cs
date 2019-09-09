using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Windows.Clipboard;


namespace Core.Export
{
    class ClipboardExport:IExport
    {
        public void Export(Cabinet cabinet)
        {
            SetData("nano", JsonConvert.SerializeObject(cabinet, Formatting.Indented));
            //SetText(JsonConvert.SerializeObject(cabinet, Formatting.Indented));
            
        }
    }
}
