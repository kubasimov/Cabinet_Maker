using System.Diagnostics;
using System.IO;
using Core.Model;
using OfficeOpenXml;

namespace Core.Export
{
    public class ExcelExport:IExport
    {
        private IExport _exportImplementation;

        public void Export(Cabinet cabinet)
        {
            
            //var excel = new Application();
            //excel.Visible = true;

            //var workbook = (Workbook) excel.Workbooks.Add();
            //var sheet = workbook.ActiveSheet;
            //sheet.Cells[1, 1] = "Numer";
            //sheet.Cells[1, 2] = "Dlugosc";
            //sheet.Cells[1, 3] = "Szerokosc";
            //sheet.Cells[1, 4] = "Ilosc";
            //sheet.Cells[1, 5] = "Okl Dlu";
            //sheet.Cells[1, 6] = "Okl Dlu";
            //sheet.Cells[1, 7] = "Okl SZER";
            //sheet.Cells[1, 8] = "Okl SZER";
            //sheet.Cells[1, 9] = "Material";
            //sheet.Cells[1, 10] = "Opis";


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
                    sheet.Cells[row, 1].Value = row;
                    sheet.Cells[row, 2].Value = element.EWidth;
                    sheet.Cells[row, 3].Value = element.EDepth;
                    sheet.Cells[row, 4].Value = 1;
                    sheet.Cells[row, 5].Value = "x";
                    sheet.Cells[row, 10].Value = element.Description;
                    ++row;
                }



                p.SaveAs(new FileInfo(@"C:\test\test.xlsx"));

            }

            Debug.WriteLine("Zakończono zapis excel");
        }
    }
}