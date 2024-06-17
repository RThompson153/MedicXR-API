﻿namespace MedicXR_API.Services.Athena.Constants
{
    internal static class AthenaConstants
    {
        internal const string AthenaEmr = "AthenaEMR";
        internal const string Services = "Services";
		#region Services
		internal const string BaseUrl = "BaseUrl";
        internal const string AuthorizationEndpoint = "AuthorizationService";
        internal const string TokenEndpoint = "TokenService";
        internal const string ChartEndpoint = "ChartService";
        internal const string AppointmentsEndpoint = "AppointmentsService";
        internal const string PatientsEndpoint = "PatientsService";
        internal const string ProvidersEndpoint = "ProvidersService";
        #endregion
        internal const string Scopes = "Scopes";
		#region Service Parameters
		internal const string AppointmentDate = "appointmentdate";
        internal const string AppointmentTime = "appointmenttime";
        internal const string AppointmentTypeId = "appointmenttypeid";
        internal const string PracticeId = "{practiceId}";
        internal const string DepartmentId = "{departmentId}";
        internal const string ProviderId = "{providerId}";
        internal const string StartDate = "{startDate}";
        internal const string EndDate = "{endDate}";
        internal const string PatientId = "{patientId}";
        internal const string ReasonId = "reasonid";
        #endregion
    }
}
