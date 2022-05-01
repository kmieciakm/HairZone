using Hairzone.CORE.Contracts;
using Hairzone.CORE.Models;
using Hairzone.CORE.Models.Exceptions;
using System.Xml.Linq;

namespace Hairzone.CORE.Services;

public class ContractorParser : IContractorParser
{
    public Contractor ParseXML(XDocument document)
    {
        try
        {
            var root = document.Root;
            var addressElement = root?.Element("ADDRESS")!;

            Address address = new(
                addressElement.Element("CITY")!.Value,
                addressElement.Element("STREET")!.Value,
                addressElement.Element("NUMBER")!.Value,
                addressElement.Element("POSTALCODE")!.Value
            );

            Contractor contractor = new(
                root.Element("NAME")!.Value,
                root.Element("PHONE")!.Value,
                root.Element("EMAIL")!.Value,
                address
            );

            return contractor;
        }
        catch (Exception ex)
        {
            throw new ParserException($"Cannot parse XML to {nameof(Contractor)} type.", document, ex);
        }
    }
}