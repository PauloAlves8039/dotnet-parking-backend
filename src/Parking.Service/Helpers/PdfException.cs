namespace Parking.Service.Helpers
{
    public class PdfException : Exception
    {
        public PdfException(string message, Exception innerException) : base(message, innerException) {}
    }

}
