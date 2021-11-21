using Config.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Config
{
    public interface IConfig
    {
        string CabinetFilesDirectory();
        string CabinetName();
        int CabinetHeight();
        int CabinetWidth();
        int CabinetDepth();
        int CabinetSizeElement();
        int CabinetBack();
        int SlotsTop();
        int SlotsBottom();
        int SlotsLeft();
        int SlotsRight();
        int SlotsBetweenVertically();
        int SlotsBetweenHorizontally();
        int SlotsBetweenCabinet();
        int EnumFront();
        string GetExcelExportDirectory();
    }
}
