using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Azure.Management.Fluent.Azure;

namespace msiarm
{
    public static class ArmFluentClient
    {
        private static AzureServiceTokenProvider _azureServiceTokenProvider = new AzureServiceTokenProvider();
        private static object _azureClient;
        private static DateTime _tokenExpiry = DateTime.MinValue;

        public static async Task<IAuthenticated> GetAzureClient(ILogger log)
        {
            if (_azureClient == null || DateTime.UtcNow >= _tokenExpiry)
            {
                if (DateTime.UtcNow >= _tokenExpiry)
                {
                    log.LogInformation($"Renewing token at {DateTime.UtcNow.ToLongDateString()}");
                }
                else
                {
                    log.LogInformation($"Creating new azureclient at {DateTime.UtcNow.ToLongDateString()}");

                }
                var tenantId = Environment.GetEnvironmentVariable("tenantId");
                var tokenCredentials = new TokenCredentials(await _azureServiceTokenProvider.GetAccessTokenAsync("https://management.azure.com/"));

                var azureCredentials = new AzureCredentials(
                    tokenCredentials,
                    tokenCredentials,
                    tenantId,
                    AzureEnvironment.AzureGlobalCloud);
                var client = RestClient
                    .Configure()
                    .WithEnvironment(AzureEnvironment.AzureGlobalCloud)
                    .WithLogLevel(HttpLoggingDelegatingHandler.Level.BodyAndHeaders)
                    .WithCredentials(azureCredentials)
                    .Build();
                _tokenExpiry = DateTime.UtcNow.AddMinutes(15);
                _azureClient = Azure.Authenticate(client, tenantId);
            }
            return (IAuthenticated)_azureClient;
        }
    }

}
