using DSC.CRM.Integration.API.Helpers;
using DSC.CRM.Integration.API.Models.Requests;
using DSC.CRM.Integration.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace DSC.CRM.Integration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstablishmentController : ControllerBase
    {
        private readonly CRMConnector _crmConnector;
        private readonly EstablishmentService _service;

        public EstablishmentController(
            CRMConnector crmConnector,
            EstablishmentService service)
        {
            _crmConnector = crmConnector;
            _service = service;
        }

        // ==============================
        // Check Dataverse Connection
        // ==============================

        [HttpGet("check-connection")]
        public IActionResult CheckConnection()
        {
            try
            {
                ServiceClient service = _crmConnector.GetCRMService();

                if (!service.IsReady)
                {
                    return BadRequest(new
                    {
                        message = "CRM Connection Failed",
                        error = service.LastError
                    });
                }

                return Ok("CRM Connection Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        // ==============================
        // WhoAmI (Dataverse test)
        // ==============================

        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            try
            {
                var service = _crmConnector.GetCRMService();

                OrganizationRequest request = new OrganizationRequest("WhoAmI");

                OrganizationResponse response = service.Execute(request);

                return Ok(new
                {
                    UserId = response.Results["UserId"],
                    BusinessUnitId = response.Results["BusinessUnitId"],
                    OrganizationId = response.Results["OrganizationId"]
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ==============================
        // Create Establishment
        // ==============================

        [HttpPost("create")]
        public IActionResult CreateEstablishment([FromBody] EstablishmentRequest request)
        {
            try
            {
                Guid id = _service.CreateEstablishment(request);

                return Ok(new
                {
                    message = "Establishment created successfully",
                    establishmentId = id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}