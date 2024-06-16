namespace MedicXR_API.Services.Athena.Constants
{
    internal static class AthenaConstants
    {
		#region Settings
        internal const string AthenaEmr = "AthenaEMR";
        internal const string BaseUrl = "BaseUrl";
        internal const string AuthorizationEndpoint = "AuthorizationEndpoint";
        internal const string TokenEndpoint = "TokenEndpoint";
        internal const string ChartEndpoint = "ChartEndpoint";
        internal const string ConditionsEndpoint = "ConditionsEndpoint";
        internal const string AppointmentsEndpoint = "AppointmentsEndpoint";
        internal const string PatientsEndpoint = "PatientsEndpoint";
        internal const string ProvidersEndpoint = "ProvidersEndpoint";
		#endregion
		#region Scopes
        internal const string AthenaScope = "Athena";
        internal const string ConditionsScope = "Condition";
        #endregion
		#region Appointments
		internal const string AppointmentDate = "appointmentdate";
        internal const string AppointmentTime = "appointmenttime";
        internal const string AppointmentTypeId = "appointmenttypeid";
        internal const string DepartmentId = "departmentid";
        internal const string ProviderId = "providerid";
        internal const string ReasonId = "reasonid";
        #endregion

        #region Patients
        #endregion
    }
}
