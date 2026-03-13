namespace DSC.CRM.Integration.API.Helpers
{
    public static class ProjectConstants
    {
        // ===============================
        // Gender
        // ===============================
        public static class Gender
        {
            public const int Male = 1;
            public const int Female = 2;
            public const int Both = 3;
            public const int Kids = 4;
        }

        // ===============================
        // Establishment Type
        // ===============================
        public static class EstablishmentType
        {
            public const int SportServiceCompany = 1;
            public const int PrivateSportsClub = 2;
            public const int SportsCompany = 3;
            public const int SportsAcademy = 4;
            public const int FitnessCenter = 5;
            public const int ESport = 6;
            public const int EventOrganizer = 7;
            public const int PrivateCompany = 8;
            public const int PublicCompany = 9;
            public const int School = 10;
            public const int University = 11;
        }

        // ===============================
        // License Source
        // ===============================
        public static class LicenseSource
        {
            public const int DET = 1;
            public const int SharjahFreezone = 2;
            public const int DubaiFreezone = 3;
            public const int InitialApproval = 4;
            public const int Default = 0;
        }

        // ===============================
        // Request Status
        // ===============================
        public static class EServiceRequestStatus
        {
            public const int Approved = 1;
            public const int Received = 2;
            public const int Rejected = 3;
            public const int MissingDocuments = 4;
            public const int ApprovedBySubAdmin = 5;
            public const int Processing = 6;
            public const int Cancelled = 7;
            public const int ConditionallyApproved = 8;
            public const int Unknown = 0;

        }
    }
}