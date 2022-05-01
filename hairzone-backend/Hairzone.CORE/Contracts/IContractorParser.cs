using Hairzone.CORE.Models;
using System.Xml.Linq;

namespace Hairzone.CORE.Contracts;

public interface IContractorParser
{
    Contractor ParseXML(XDocument document);
}