using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Extensions.Logging;
using Parking.Model.DTOs;
using Parking.Service.Helpers.Interfaces;

namespace Parking.Service.Helpers;

public class PdfService : IPdfService
{

    private readonly ILogger<PdfService> _logger;

    public PdfService(ILogger<PdfService> logger)
    {
        _logger = logger;
    }

    public byte[] GenerateStayPdf(StayDTO stayDto)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            var table = new Table(UnitValue.CreatePercentArray([1, 2]))
                .SetWidth(UnitValue.CreatePercentValue(100));

            table.AddHeaderCell(new Cell().Add(new Paragraph("Descrição")).SetBold().SetFontColor(ColorConstants.BLUE));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Valor")).SetBold().SetFontColor(ColorConstants.BLUE));

            table.AddCell("Código do Cliente - Veículo:");
            table.AddCell(stayDto.CustomerVehicleId.HasValue ? stayDto.CustomerVehicleId.Value.ToString() : "N/A");

            table.AddCell("Placa:");
            table.AddCell(stayDto.LicensePlate ?? "N/A");

            table.AddCell("Data de Entrada:");
            table.AddCell(stayDto.EntryDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A");

            table.AddCell("Data de Saída:");
            table.AddCell(stayDto.ExitDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A");

            table.AddCell("Taxa Por Hora:");
            table.AddCell(stayDto.HourlyRate.ToString("C"));

            table.AddCell("Total:");
            table.AddCell(stayDto.TotalAmount?.ToString("C") ?? "N/A");

            table.AddCell("Status:");
            table.AddCell(stayDto.StayStatus ?? "N/A");

            document.Add(new Paragraph("Estacionamento de Veículos")
                .SetBold()
                .SetFontSize(18)
                .SetTextAlignment(TextAlignment.CENTER).SetFontColor(ColorConstants.BLUE));
            document.Add(table);

            document.Add(new Paragraph("Desenvolvido por - Paulo Alves")
                .SetBold()
                .SetFontSize(8)
                .SetTextAlignment(TextAlignment.RIGHT));

            document.Close();
            return memoryStream.ToArray();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating PDF for Stay ID: {Id}", stayDto?.Id);
            throw new PdfStayException("An error occurred while generating the PDF.", ex);
        }
    }

}
