using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Extensions.Configuration;

namespace DSC.CRM.Integration.API.Helpers
{
    public class CRMConnector
    {
        private readonly IConfiguration _configuration;

        public CRMConnector(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ServiceClient GetCRMService()
        {
            var crmUrl = _configuration["CRMSettings:Url"];
            var clientId = _configuration["CRMSettings:ClientId"];
            var clientSecret = _configuration["CRMSettings:ClientSecret"];
            var tenantId = _configuration["CRMSettings:TenantId"];

            string connectionString =
                $"AuthType=ClientSecret;" +
                $"Url={crmUrl};" +
                $"ClientId={clientId};" +
                $"ClientSecret={clientSecret};" +
                $"TenantId={tenantId};";

            ServiceClient serviceClient = new ServiceClient(connectionString);

            return serviceClient;
        }
    }
}