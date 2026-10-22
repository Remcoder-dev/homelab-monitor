using System.Text.Json;
using System.Net.Http.Json;

namespace HomelabMonitor.Api.Clients;

public class ProxmoxClient(HttpClient httpClient)
{
  public async Task<JsonElement> GetResourcesAsync(CancellationToken cancellationToken = default)
  {
    var response = await httpClient.GetFromJsonAsync<JsonElement>("cluster/resources", cancellationToken);
    return response;
  }
}