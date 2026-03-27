namespace DSC.CRM.Integration.API.Models.Requests
{
    public class OwnerRequest
    {
        public string Name { get; set; }

        public int PercentageOwned { get; set; }

        public ContactDetailsRequest ContactDetails { get; set; }
    }
}