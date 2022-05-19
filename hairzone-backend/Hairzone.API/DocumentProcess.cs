using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hairzone.CORE.Contracts;
using Hairzone.CORE.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Hairzone.API;

[StorageAccount("DocumentsBlob_ConnectionString")]
public class DocumentProcess
{
    private IContractorParser _ContractorParser { get; }
    private ISalonAdministrationService _SalonAdministrationService { get; }

    public DocumentProcess(
        IContractorParser contractorParser,
        ISalonAdministrationService salonAdministrationService)
    {
        _ContractorParser = contractorParser;
        _SalonAdministrationService = salonAdministrationService;
    }

    [FunctionName("ContractorProcess")]
    public async Task ContractorProcess(
        [BlobTrigger("documents-input/{name}")] Stream inputDocument,
        [Blob("documents-error/{name}", FileAccess.Write)] Stream outputDocument,
        string name,
        ILogger log)
    {
        try
        {
            log.LogInformation($"Start Processing document \n {name} \n Size: {inputDocument.Length} Bytes");

            XDocument document = XDocument.Load(inputDocument);
            Contractor contractor = _ContractorParser.ParseXML(document);
            Salon salon = new Salon(contractor);
            await _SalonAdministrationService.RegisterSalonAsync(salon);
        }
        catch (Exception ex)
        {
            log.LogError($"Processing Contractor document failed. Name: {name}", ex);
            await inputDocument.CopyToAsync(outputDocument);
        }
    }
}
