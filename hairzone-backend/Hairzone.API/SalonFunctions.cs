using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Hairzone.CORE.Contracts;
using Hairzone.CORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Hairzone.API;

public class SalonFunctions
{
    private readonly ILogger<SalonFunctions> _logger;
    private ISalonAdministrationService _salonService { get; }

    public SalonFunctions(
        ILogger<SalonFunctions> log,
        ISalonAdministrationService salonService)
    {
        _logger = log;
        _salonService = salonService;
    }

    [FunctionName("GetCities")]
    [OpenApiOperation(operationId: "GetCities", tags: new[] { "city" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<Salon>), Description = "The list of cities.")]
    public async Task<IActionResult> GetCities(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "city")] HttpRequest req)
    {
        try
        {
            var cities = await _salonService.GetCities();
            return new OkObjectResult(cities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(GetCities)}");
            return new DetailedStatusCodeResult(500, "");
        }
    }

    [FunctionName("GetSalons")]
    [OpenApiOperation(operationId: "GetSalons", tags: new[] { "salon" })]
    [OpenApiParameter(name: "city", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The city to search salons in.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<Salon>), Description = "The list of salons.")]
    public async Task<IActionResult> GetSalons(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "salon")] HttpRequest req)
    {
        try
        {
            string city = req.Query["city"];
            _logger.LogInformation("Searching salons in {city} ...", city);
            var salons = await _salonService.GetAvailableSalonsInCity(city);
            return new OkObjectResult(salons);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(GetSalons)}");
            return new DetailedStatusCodeResult(500, "");
        }
    }
}

public class DetailedStatusCodeResult : ActionResult
{
    public int StatusCode { get; }
    public object Details { get; set; }

    public DetailedStatusCodeResult(int statusCode, object details)
    {
        StatusCode = statusCode;
        Details = details;
    }

    public override void ExecuteResult(ActionContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        context.HttpContext.Response.StatusCode = StatusCode;

        string body = System.Text.Json.JsonSerializer.Serialize(Details);
        context.HttpContext.Response.WriteAsync(body);
    }
}
