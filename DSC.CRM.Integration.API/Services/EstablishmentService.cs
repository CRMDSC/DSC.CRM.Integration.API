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

            // ===============================
            // Create Establishment Request
            // ===============================

            Entity establishment = new Entity("dsc_establishmentrequest");

            establishment["dsc_newcolumn"] = request.TitleEn;
            establishment["dsc_titlearabic"] = request.TitleAr;
            establishment["dsc_descriptionenglish"] = request.DescriptionEn;
            establishment["dsc_descriptionarabic"] = request.DescriptionAr;

            establishment["dsc_address"] = request.Address;
            //establishment["dsc_latitude"] = Convert.ToDouble(request.Lat);
            //establishment["dsc_longitude"] = Convert.ToDouble(request.Lng);

            establishment["dsc_makaninumber"] = request.MakaniNumber;
            establishment["dsc_pobox"] = request.PoBox;

            establishment["dsc_tradelicensenumber"] = request.TradeLicense;
            establishment["dsc_tradelicensestartdate"] = request.TradeLicenseStartDate;
            establishment["dsc_tradelicenseexpirydate"] = request.TradeLicenseExpiryDate;

            establishment["dsc_establishmentphoto"] = request.EstablishmentPhoto;

            //// Latitude
            //if (!string.IsNullOrEmpty(request.Lat))
            //{
            //    establishment["dsc_latitude"] = request.Lat;
            //}

            //// Longitude
            //if (!string.IsNullOrEmpty(request.Lng))
            //{
            //    establishment["dsc_longitude"] = request.Lng;
            //}

            //// Country ID (TEXT)
            //if (!string.IsNullOrEmpty(request.CountryID))
            //{
            //    establishment["dsc_countryid"] = request.CountryID;
            //}

            //// Total Staff (WHOLE NUMBER)
            //if (request.TotalStaff.HasValue)
            //{
            //    establishment["dsc_totalstaff"] = request.TotalStaff.Value;
            //}

            // Private Contact
            establishment["dsc_privateemail"] = request.EmailPrivate;
            establishment["dsc_privatephone"] = request.PhonePrivate;

            // Tags
            establishment["dsc_tags"] = request.Tags;

            // Timestamp
            establishment["dsc_timestamp"] = request.TimeStamp;

            // Contact Details
            if (request.ContactDetails != null)
            {
                establishment["dsc_contactphone"] = request.ContactDetails.Phone;
                establishment["dsc_contactemail"] = request.ContactDetails.Email;
                establishment["dsc_contactwebsite"] = request.ContactDetails.Website;
            }

            Guid establishmentId = service.Create(establishment);

            // ===============================
            // Create Establishment Owners
            // ===============================

            if (request.EstablishmentOwners != null)
            {
                foreach (var owner in request.EstablishmentOwners)
                {
                    Entity ownerEntity = new Entity("dsc_establishmentowner");

                    ownerEntity["dsc_ownernamer"] = owner.Name;
                    ownerEntity["dsc_nationality"] = owner.Nationality;
                    ownerEntity["dsc_phone"] = owner.Phone;
                    ownerEntity["dsc_email"] = owner.Email;

                    ownerEntity["dsc_establishmentrequest"] =
                        new EntityReference("dsc_establishmentrequest", establishmentId);

                    service.Create(ownerEntity);
                }
            }
            //
            // ===============================
            // Create Establishment Managers
            // ===============================
            //
            if (request.EstablishmentManagers != null)
            {
                foreach (var manager in request.EstablishmentManagers)
                {
                    Entity managerEntity = new Entity("dsc_establishmentmanager1");

                    managerEntity["dsc_managername"] = manager.Name;
                    managerEntity["dsc_phone"] = manager.Phone;
                    managerEntity["dsc_email"] = manager.Email;

                    managerEntity["dsc_establishmentrequest"] =
                        new EntityReference("dsc_establishmentrequest", establishmentId);

                    service.Create(managerEntity);
                }
            }

            return establishmentId;
        }
    }
}