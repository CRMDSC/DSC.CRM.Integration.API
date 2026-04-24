using DSC.CRM.Integration.API.Helpers;
using DSC.CRM.Integration.API.Models.Requests;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace DSC.CRM.Integration.API.Services
{
    public class EstablishmentService
    {
        private readonly CRMConnector _crmConnector;

        public EstablishmentService(CRMConnector crmConnector)
        {
            _crmConnector = crmConnector;
        }

        public Guid CreateEstablishment(EstablishmentRequest request)
        {
            var service = _crmConnector.GetCRMService();

            Entity establishment = new Entity("dsc_establishmentrequest");

            // ================= BASIC =================
            establishment["dsc_newcolumn"] = request.TitleEn ?? "";
            establishment["dsc_titlearabic"] = request.TitleAr ?? "";
            establishment["dsc_descriptionenglish"] = request.DescriptionEn ?? "";
            establishment["dsc_descriptionarabic"] = request.DescriptionAr ?? "";
            establishment["dsc_address"] = request.Address ?? "";

            establishment["dsc_makaninumber"] = request.MakaniNumber ?? "";
            establishment["dsc_pobox"] = request.PoBox ?? "";

            establishment["dsc_tradelicensenumber"] = request.TradeLicense ?? "";

            //if (request.TradeLicenseStartDate != null)
            //    establishment["dsc_tradelicensestartdate"] = request.TradeLicenseStartDate;

            //if (request.TradeLicenseExpiryDate != null)
            //    establishment["dsc_tradelicenseexpirydate"] = request.TradeLicenseExpiryDate;

            establishment["dsc_establishmentphoto"] = request.EstablishmentPhoto ?? "";

            // ================= LOCATION =================
            if (request.Lat != 0)
                establishment["dsc_latitude"] = request.Lat.ToString();

            if (request.Lng != 0)
                establishment["dsc_longitude"] = request.Lng.ToString();

            // ================= NUMBERS =================
            //if (request.TotalStaff > 0)
            //    establishment["dsc_totalstaff"] = request.TotalStaff;

            //if (request.CountryID > 0)
            //    establishment["dsc_countryid"] = request.CountryID.ToString();

            // ================= PRIVATE =================
            establishment["dsc_privateemail"] = request.EmailPrivate ?? "";
            establishment["dsc_privatephone"] = request.PhonePrivate ?? "";

            // ================= GENDER =================
            switch (request.Gender)
            {
                case ProjectConstants.Gender.Male:
                    establishment["dsc_gender"] = new OptionSetValue(ProjectConstants.Gender.Male);
                    break;

                case ProjectConstants.Gender.Female:
                    establishment["dsc_gender"] = new OptionSetValue(ProjectConstants.Gender.Female);
                    break;

                case ProjectConstants.Gender.Both:
                    establishment["dsc_gender"] = new OptionSetValue(ProjectConstants.Gender.Both);
                    break;

                case ProjectConstants.Gender.Kids:
                    establishment["dsc_gender"] = new OptionSetValue(ProjectConstants.Gender.Kids);
                    break;

                default:
                    establishment["dsc_gender"] = new OptionSetValue(ProjectConstants.Gender.Male);
                    break;
            }

            // ================= ESTABLISHMENT TYPE =================
            switch (request.EstablishmentType)
            {
                case ProjectConstants.EstablishmentType.SportServiceCompany:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.SportServiceCompany);
                    break;

                case ProjectConstants.EstablishmentType.PrivateSportsClub:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.PrivateSportsClub);
                    break;

                case ProjectConstants.EstablishmentType.SportsCompany:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.SportsCompany);
                    break;

                case ProjectConstants.EstablishmentType.SportsAcademy:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.SportsAcademy);
                    break;

                case ProjectConstants.EstablishmentType.FitnessCenter:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.FitnessCenter);
                    break;

                case ProjectConstants.EstablishmentType.ESport:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.ESport);
                    break;

                case ProjectConstants.EstablishmentType.EventOrganizer:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.EventOrganizer);
                    break;

                case ProjectConstants.EstablishmentType.PrivateCompany:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.PrivateCompany);
                    break;

                case ProjectConstants.EstablishmentType.PublicCompany:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.PublicCompany);
                    break;

                case ProjectConstants.EstablishmentType.School:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.School);
                    break;

                case ProjectConstants.EstablishmentType.University:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.University);
                    break;

                default:
                    establishment["dsc_establishmenttype"] = new OptionSetValue(ProjectConstants.EstablishmentType.SportServiceCompany);
                    break;
            }

            // ================= LICENSE SOURCE =================
            switch (request.LicenseSourceID)
            {
                case ProjectConstants.LicenseSource.DET:
                    establishment["dsc_licensesource"] = new OptionSetValue(ProjectConstants.LicenseSource.DET);
                    break;

                case ProjectConstants.LicenseSource.SharjahFreezone:
                    establishment["dsc_licensesource"] = new OptionSetValue(ProjectConstants.LicenseSource.SharjahFreezone);
                    break;

                case ProjectConstants.LicenseSource.DubaiFreezone:
                    establishment["dsc_licensesource"] = new OptionSetValue(ProjectConstants.LicenseSource.DubaiFreezone);
                    break;

                case ProjectConstants.LicenseSource.InitialApproval:
                    establishment["dsc_licensesource"] = new OptionSetValue(ProjectConstants.LicenseSource.InitialApproval);
                    break;

                default:
                    establishment["dsc_licensesource"] = new OptionSetValue(ProjectConstants.LicenseSource.Default);
                    break;
            }

            // ================= CONTACT =================
            if (request.ContactDetails != null)
            {
                establishment["dsc_contactphone"] = request.ContactDetails.Phone ?? "";
                establishment["dsc_contactemail"] = request.ContactDetails.Email ?? "";
                establishment["dsc_contactwebsite"] = request.ContactDetails.Website ?? "";
            }

            Guid establishmentId = service.Create(establishment);

            // ================= OWNERS =================
            if (request.EstablishmentOwners != null)
            {
                foreach (var owner in request.EstablishmentOwners)
                {
                    Entity ownerEntity = new Entity("dsc_establishmentowner");

                    ownerEntity["dsc_ownernamer"] = owner.Name ?? "";
                    ownerEntity["dsc_percentageowned"] = owner.PercentageOwned;

                    // 👇 FIX (from contactDetails)
                    if (owner.ContactDetails != null)
                    {
                        ownerEntity["dsc_email"] = owner.ContactDetails.Email ?? "";
                        ownerEntity["dsc_phone"] = owner.ContactDetails.Phone ?? "";
                    }

                    ownerEntity["dsc_establishmentrequest"] =
                        new EntityReference("dsc_establishmentrequest", establishmentId);

                    service.Create(ownerEntity);
                }
            }

            // ================= MANAGERS =================
            if (request.EstablishmentManagers != null)
            {
                foreach (var manager in request.EstablishmentManagers)
                {
                    Entity managerEntity = new Entity("dsc_establishmentmanager1");

                    managerEntity["dsc_managername"] = manager.Name ?? "";

                    // 👇 FIX
                    if (manager.ContactDetails != null)
                    {
                        managerEntity["dsc_email"] = manager.ContactDetails.Email ?? "";
                        managerEntity["dsc_phone"] = manager.ContactDetails.Phone ?? "";
                    }

                    managerEntity["dsc_countryid"] = manager.CountryID;

                    managerEntity["dsc_establishmentrequest"] =
                        new EntityReference("dsc_establishmentrequest", establishmentId);

                    service.Create(managerEntity);
                }
            }

            return establishmentId;
        }
    }
}