using ClosedXML.Excel;
using labo_10.Domain.Interfaces;

namespace labo_10.Infrastructure.Services;

public class ClosedXmlService : IClosedXmlService
{
    public void FirstExample()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("FirstExample");
            worksheet.Cell(1, 1).Value = "User 1";
            worksheet.Cell(1, 2).Value = "User 2";
            worksheet.Cell(1, 3).Value = "User 3";
            worksheet.Cell(1, 4).Value = "User 4";
            worksheet.Cell(1, 5).Value = "User 5";
            
            workbook.SaveAs("users.xlsx");
        }
    }

    public void ModifyArchive()
    {
        using (var workbook = new XLWorkbook("users.xlsx"))
        {
            var worksheet = workbook.Worksheet(1);
            worksheet.Cell(2, 2).Value = "User 1 modified";
            
            workbook.Save();
        }
    }

    public void ThirdExample()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Datos");
            worksheet.Cell(1, 1).Value = "id";
            worksheet.Cell(1, 2).Value = "name";
            worksheet.Cell(1, 3).Value = "age";
            
            worksheet.Cell(2, 1).Value = 1;
            worksheet.Cell(2, 2).Value = "Pedro";
            worksheet.Cell(2, 3).Value = 28;
            
            worksheet.Cell(2, 1).Value = 2;
            worksheet.Cell(2, 2).Value = "maria";
            worksheet.Cell(2, 3).Value = 22;
            
            var range = worksheet.Range("A1:C3");
            range.CreateTable();
            
            workbook.SaveAs("tabla.xlsx");
        }
    }

    public void FourthExample()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("estilos");
            
            var headerRow = worksheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.Green;
            headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            
            worksheet.Cell(1, 1).Value = "id";
            worksheet.Cell(1, 2).Value = "nombre";
            worksheet.Cell(1, 3).Value = "edad";
            
            worksheet.Cell(2, 1).Value = 1;
            worksheet.Cell(2, 2).Value = "pepe";
            worksheet.Cell(2, 3).Value = 28;
            
            worksheet.Cell(2, 1).Value = 2;
            worksheet.Cell(2, 2).Value = "juan";
            worksheet.Cell(2, 3).Value = 22;
            
            workbook.SaveAs("archivo_stilos.xlsx");
        }
    }
}