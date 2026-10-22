namespace HomelabMonitor.Api.Configuration;
public class ProxmoxOptions
{
  public const string SectionName = "ProxmoxSettings";

  public string Host { get; set; } = string.Empty;
  public string Port { get; set; } = string.Empty;
  public string TokenId { get; set; } = string.Empty;
  public string TokenSecret { get; set; } = string.Empty;
}