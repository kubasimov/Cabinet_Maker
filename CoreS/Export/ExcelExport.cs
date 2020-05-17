using Config;
using CoreS.Enum;
using OfficeOpenXml;
using System.Diagnostics;
using System.IO;

namespace CoreS.Export
{
    public class ExcelExport:IExport
    {
        private IConfig _config;

        public ExcelExport(IConfig config)
        {
            _config = config;
        }

        public void Export(Cabinet cabinet)
        {
            using (var p = new ExcelPackage())
            {
                var sheet = p.Workbook.Worksheets.Add("New");
                sheet.Cells["A1"].Value = "Numer";
                sheet.Cells["B1"].Value = "Dlugosc";
                sheet.Cells["C1"].Value = "Szerokosc";
                sheet.Cells["D1"].Value = "Ilosc";
                sheet.Cells["E1"].Value = "Okl Dlu";
                sheet.Cells["F1"].Value = "Okl Dlu";
                sheet.Cells[1, 7].Value = "Okl SZER";
                sheet.Cells[1, 8].Value = "Okl SZER";
                sheet.Cells[1, 9].Value = "Material";
                sheet.Cells[1, 10].Value = "Opis";

                var row = 2;

                foreach (var element in cabinet.CabinetElements)
                {
                    sheet.Cells[row, 1].Value = row-1;

                    switch (element.GetEnumName())
                    {
                        case EnumCabinetElement.Leftside:
                        case EnumCabinetElement.Rightside:
                            sheet.Cells[row, 2].Value = element.Height;
                            sheet.Cells[row, 3].Value = element.Depth;
                            sheet.Cells[row, 5].Value = "x";
                            break;
                        case EnumCabinetElement.Back:
                            sheet.Cells[row, 2].Value = element.Height;
                            sheet.Cells[row, 3].Value = element.Width;
                            break;
                        case EnumCabinetElement.VerticalBarrier:
                            break;
                        case EnumCabinetElement.HorizontalBarrier:
                            break;
                        case EnumCabinetElement.Front:
                            break;
                        default:
                            sheet.Cells[row, 2].Value = element.Width;
                            sheet.Cells[row, 3].Value = element.Depth;
                            sheet.Cells[row, 5].Value = "x";
                            break;
                    }

                    sheet.Cells[row, 4].Value = 1;
                    
                    sheet.Cells[row, 10].Value = element.Description;
                    ++row;
                }

                var elementH = cabinet.HorizontalBarrier[0];

                sheet.Cells[row, 1].Value = row - 1;
                sheet.Cells[row, 2].Value = elementH.Width;
                sheet.Cells[row, 3].Value = elementH.Depth;
                sheet.Cells[row, 4].Value = cabinet.HorizontalBarrier.Count;
                sheet.Cells[row, 5].Value = "x";
                sheet.Cells[row, 10].Value = elementH.Description;
                ++row;

                var elementV = cabinet.VerticalBarrier[0];
                sheet.Cells[row, 1].Value = row - 1;
                sheet.Cells[row, 2].Value = elementV.Height;
                sheet.Cells[row, 3].Value = elementV.Depth;
                sheet.Cells[row, 4].Value = cabinet.VerticalBarrier.Count;
                sheet.Cells[row, 5].Value = "x";
                sheet.Cells[row, 10].Value = elementV.Description;
                ++row;

                p.SaveAs(new FileInfo(_config.GetExcelExportDirectory()));

            }
        }
    }
}