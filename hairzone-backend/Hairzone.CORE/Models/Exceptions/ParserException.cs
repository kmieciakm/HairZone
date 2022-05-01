using System.Xml.Linq;

namespace Hairzone.CORE.Models.Exceptions;

public class ParserException : Exception
{
    public XDocument Document { get; set; }

    public ParserException(string? message, XDocument document)
        : base(message)
    {
        Document = document;
    }

    public ParserException(string? message, XDocument document, Exception? innerException)
        : base(message, innerException)
    {
        Document = document;
    }
}