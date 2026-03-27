namespace DSC.CRM.Integration.API.Models.Requests
{
    public class ManagerRequest
    {
        public string Name { get; set; }

        public int CountryID { get; set; }

        public ContactDetailsRequest ContactDetails { get; set; }
    }
}