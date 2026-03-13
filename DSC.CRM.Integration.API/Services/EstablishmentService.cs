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
            //
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

            // Latitude
            if (request.Lat != 0)
            {
                establishment["dsc_latitude"] = request.Lat.ToString();
            }

            // Longitude
            if (request.Lng != 0)
            {
                establishment["dsc_longitude"] = request.Lng.ToString();
            }

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
            if (request.TotalStaff > 0)
            {
                establishment["dsc_totalstaff"] = request.TotalStaff;
            }
            if (request.CountryID > 0)
            {
                establishment["dsc_countryid"] = request.CountryID.ToString();
            }

            // Private Contact
            establishment["dsc_privateemail"] = request.EmailPrivate;
            establishment["dsc_privatephone"] = request.PhonePrivate;

            //// Gender Mapping
            //if (request.Gender == ProjectConstants.Gender.Male)
            //{
            //    establishment["dsc_gender"] = ProjectConstants.Gender.Male;
            //}
            //else if (request.Gender == ProjectConstants.Gender.Female)
            //{
            //    establishment["dsc_gender"] = ProjectConstants.Gender.Female;
            //}
            //else if (request.Gender == ProjectConstants.Gender.Both)
            //{
            //    establishment["dsc_gender"] = ProjectConstants.Gender.Both;
            //}
            //else if (request.Gender == ProjectConstants.Gender.Kids)
            //{
            //    establishment["dsc_gender"] = ProjectConstants.Gender.Kids;
            //}
            //// EStablishment Type
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

            //switch (request.Status)
            //{
            //    case ProjectConstants.EServiceRequestStatus.Approved:
            //        establishment["dsc_status"] = ProjectConstants.EServiceRequestStatus.Approved;
            //        break;

            //    case ProjectConstants.EServiceRequestStatus.Received:
            //        establishment["dsc_status"] = ProjectConstants.EServiceRequestStatus.Received;
            //        break;

            //    case ProjectConstants.EServiceRequestStatus.Rejected:
            //        establishment["dsc_status"] = ProjectConstants.EServiceRequestStatus.Rejected;
            //        break;

            //    case ProjectConstants.EServiceRequestStatus.MissingDocuments:
            //        establishment["dsc_status"] = ProjectConstants.EServiceRequestStatus.MissingDocuments;
            //        break;

            //    case ProjectConstants.EServiceRequestStatus.ApprovedBySubAdmin:
            //        establishment["dsc_status"] = ProjectConstants.EServiceRequestStatus.ApprovedBySubAdmin;
            //        break;

            //    case ProjectConstants.EServiceRequestStatus.Processing:
            //        establishment["dsc_status"] = ProjectConstants.EServiceRequestStatus.Processing;
            //        break;

            //    case ProjectConstants.EServiceRequestStatus.Cancelled:
            //        establishment["dsc_status"] = ProjectConstants.EServiceRequestStatus.Cancelled;
            //        break;

            //    case ProjectConstants.EServiceRequestStatus.ConditionallyApproved:
            //        establishment["dsc_status"] = ProjectConstants.EServiceRequestStatus.ConditionallyApproved;
            //        break;

            //    default:
            //        establishment["dsc_status"] = ProjectConstants.EServiceRequestStatus.Unknown;
            //        break;
            //}

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