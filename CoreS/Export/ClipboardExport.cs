using CoreS;
using CoreS.Export;
using CoreS.Helpers;
using Newtonsoft.Json;


namespace Core.Export
{
    internal class ClipboardExport: MyLogger, IExport
    {
        public void Export(Cabinet cabinet)
        {
            Logger.Info("ClipboardExport");
            TextCopy.Clipboard.SetText(JsonConvert.SerializeObject(cabinet));
            //SetData("nano", JsonConvert.SerializeObject(cabinet, Formatting.Indented));
        }
    }
}
