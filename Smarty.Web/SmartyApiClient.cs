using SmartyStreets.USStreetApi;
using ZipCodeBatch = SmartyStreets.USZipCodeApi.Batch;

namespace Smarty.Web;

public class SmartyApiClient(HttpClient httpClient)
{
    public async Task<List<Candidate>> GetSingleAddressAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetFromJsonAsync<List<Candidate>>("/api/Smarty/single-address", cancellationToken);
        return response!;
    }

    public async Task<ZipCodeBatch> GetZipCodeAddressAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetFromJsonAsync<ZipCodeBatch>("/api/Smarty/zip-code-address", cancellationToken);
        return response!;
    }
}