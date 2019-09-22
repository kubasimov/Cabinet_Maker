using Newtonsoft.Json;
using static System.Windows.Clipboard;


namespace Core.Export
{
    internal class ClipboardExport:IExport
    {
        public void Export(Cabinet cabinet)
        {
            SetData("nano", JsonConvert.SerializeObject(cabinet, Formatting.Indented));
        }
    }
}
