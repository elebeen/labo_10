using ClosedXML.Excel;
using labo_10.Domain.Interfaces.Services;
using labo_10.Domain.Models;

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

    public byte[] GenerateTicketsReport(IEnumerable<Ticket> tickets)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Reporte de Tickets");
            
            // Configurar Cabeceras
            var headerRow = worksheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(1, 1).Value = "ID Ticket";
            worksheet.Cell(1, 2).Value = "Título";
            worksheet.Cell(1, 3).Value = "Estado";
            worksheet.Cell(1, 4).Value = "Creado por";
            worksheet.Cell(1, 5).Value = "Respuestas (Usuario: Mensaje)";

            int row = 2;
            foreach (var ticket in tickets)
            {
                worksheet.Cell(row, 1).Value = ticket.TicketId.ToString();
                worksheet.Cell(row, 2).Value = ticket.Title;
                worksheet.Cell(row, 3).Value = ticket.Status;
                worksheet.Cell(row, 4).Value = ticket.User?.Username ?? "Desconocido";
                
                // Formatear las respuestas en una sola celda
                var respuestas = ticket.Responses
                    .Select(r => $"[{r.Responder?.Username}]: {r.Message}");
                worksheet.Cell(row, 5).Value = string.Join(" | ", respuestas);
                
                row++;
            }

            worksheet.Columns().AdjustToContents(); // Auto-ajustar ancho de columnas
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }

    public byte[] GenerateUsersRolesReport(IEnumerable<User> users)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Reporte de Usuarios");
            
            // Configurar Cabeceras
            var headerRow = worksheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.LightBlue;
            headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(1, 1).Value = "ID Usuario";
            worksheet.Cell(1, 2).Value = "Username";
            worksheet.Cell(1, 3).Value = "Email";
            worksheet.Cell(1, 4).Value = "Roles Asignados";

            int row = 2;
            foreach (var user in users)
            {
                worksheet.Cell(row, 1).Value = user.UserId.ToString();
                worksheet.Cell(row, 2).Value = user.Username;
                worksheet.Cell(row, 3).Value = user.Email;
                
                // Obtener los nombres de los roles mapeados a través de UserRoles
                var roles = user.UserRoles.Select(ur => ur.Role?.RoleName);
                worksheet.Cell(row, 4).Value = string.Join(", ", roles);
                
                row++;
            }

            worksheet.Columns().AdjustToContents();
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}