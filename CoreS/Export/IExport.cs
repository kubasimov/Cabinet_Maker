using CoreS.Helpers;
using CoreS.Model;
using System.Threading.Tasks;

namespace CoreS.Export
{
    public interface IExport
    {
        Task ExportAsync(CabinetModelDTO cabinet);
    }
}