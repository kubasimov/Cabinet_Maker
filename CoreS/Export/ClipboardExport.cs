using CoreS;
using CoreS.Export;
using CoreS.Helpers;
using CoreS.Model;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Core.Export
{
    internal class ClipboardExport: MyLogger, IExport
    {
        public async Task ExportAsync(CabinetModelDTO cabinet)
        {
            Logger.Info("ClipboardExport");
            await TextCopy.ClipboardService.SetTextAsync(JsonConvert.SerializeObject(cabinet));
            
        }
    }
}
