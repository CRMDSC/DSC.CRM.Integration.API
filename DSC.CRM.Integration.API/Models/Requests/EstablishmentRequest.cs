using System;
using System.Collections.Generic;

namespace DSC.CRM.Integration.API.Models.Requests
{
    public class EstablishmentRequest
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

        public int Gender { get; set; }
        public string TradeLicense { get; set; }
        public int EstablishmentType { get; set; }

        public string MakaniNumber { get; set; }
        public string PoBox { get; set; }

        public string TitleEn { get; set; }
        public string TitleAr { get; set; }

        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }

        public string Address { get; set; }

        public string EstablishmentPhoto { get; set; }

        public List<int> SportTypeIDs { get; set; }
        public List<int> GenderGroupIDs { get; set; }

        public ContactDetailsRequest ContactDetails { get; set; }

        public List<OwnerRequest> EstablishmentOwners { get; set; }
        public List<ManagerRequest> EstablishmentManagers { get; set; }

        //public string TradeLicenseUpload { get; set; }
        public string InitialApprovalUpload { get; set; }

        public int LicenseSourceID { get; set; }

        public string OtherSportType { get; set; }

        public string EmailPrivate { get; set; }
        public string PhonePrivate { get; set; }

        public int UserID { get; set; }
        public int SportEstablishmentID { get; set; }
    }
}