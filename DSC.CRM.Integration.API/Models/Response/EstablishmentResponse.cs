using System;

namespace DSC.CRM.Integration.API.Models.Responses
{
    public class EstablishmentResponse
    {
        // Indicates if request succeeded
        public bool Success { get; set; }

        // Message for client system
        public string Message { get; set; }

        // CRM created record ID
        public Guid EstablishmentId { get; set; }

        // Timestamp of API response
        public DateTime Timestamp { get; set; }
    }
}