using System.Xml;
using CoreS;
using CoreS.Export;
using Newtonsoft.Json;


namespace Core.Export
{
    internal class ClipboardExport:IExport
    {
        public void Export(Cabinet cabinet)
        {
            TextCopy.Clipboard.SetText(JsonConvert.SerializeObject(cabinet));
            //SetData("nano", JsonConvert.SerializeObject(cabinet, Formatting.Indented));
        }
    }
}
