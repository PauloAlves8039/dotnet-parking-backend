using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Extensions.Logging;
using Parking.Model.DTOs;
using Parking.Service.Helpers.Interfaces;

namespace Parking.Service.Helpers
{
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

                table.AddHeaderCell(new Cell().Add(new Paragraph("Descrição")).SetBold());
                table.AddHeaderCell(new Cell().Add(new Paragraph("Valor")).SetBold());

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
                    .SetBold().SetFontSize(16).SetTextAlignment(TextAlignment.CENTER));
                document.Add(table);

                document.Close();
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating PDF for Stay ID: {Id}", stayDto?.Id);
                throw new PdfException("An error occurred while generating the PDF.", ex);
            }
        }

    }

}
