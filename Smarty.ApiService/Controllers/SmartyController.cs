using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using SmartyStreets;
using SmartyStreets.USStreetApi;
using ZipCodeBatch = SmartyStreets.USZipCodeApi.Batch;

namespace Smarty.ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SmartyController(IConfiguration configuration) : ControllerBase
{
    [HttpGet("single-address")]
    public ActionResult<List<Candidate>> GetAddress()
    {
        var authId = configuration.GetSection("Smarty")["AuthId"];
        var authToken = configuration.GetSection("Smarty")["AuthToken"];
        var client = new ClientBuilder(authId, authToken).WithLicense(["us-core-cloud"]).BuildUsStreetApiClient();

        var lookup = new Lookup
        {
            Addressee = "John Doe",
            Street = "1600 Amphitheatre Pkwy",
            Street2 = "closet under the stairs",
            Secondary = "APT 2",
            City = "Mountain View",
            State = "CA",
            ZipCode = "21229",
            MaxCandidates = 3
        };

        client.Send(lookup);
        return Ok(lookup.Result);
    }

    [HttpGet("zip-code-address")]
    public ActionResult<ZipCodeBatch> GetCounties()
    {
        var authId = configuration.GetSection("Smarty")["AuthId"];
        var authToken = configuration.GetSection("Smarty")["AuthToken"];
        var client = new ClientBuilder(authId, authToken).WithLicense(["us-core-cloud"]).BuildUsZipCodeApiClient();

        SmartyStreets.USZipCodeApi.Lookup[] lookups = [
            new(zipcode: "20500"),
            new(zipcode: "20500"),
            new(zipcode: "20500"),
            new(zipcode: "20500")
        ];

        var batch = new ZipCodeBatch();
        batch.AddRange(lookups);

        client.Send(batch);
        return Ok(batch);
    }
}