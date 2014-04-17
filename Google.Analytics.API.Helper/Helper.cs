using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace Google.Analytics.API.v3
{
	public class Helper
	{

		private AnalyticsService service;

		/// <summary>
		/// Constructor for the Google Analytics Helper
		/// </summary>
		/// <param name="serviceAccountEmail">Service account email for the Google API. Looks like xxxxxxxxx-xxxxxxxxxx@developer.gserviceaccount.com</param>
		/// <param name="applicationName">The name of the Google API Application</param>
		/// <param name="p12CertificatePath">Path to the downloaded p12 certificate</param>
		/// <param name="certificatePassword">Password to the p12 certificate - default "notasecret" </param>
		public Helper(string serviceAccountEmail, string applicationName, string p12CertificatePath, string certificatePassword)
		{
			
			var certificate = new X509Certificate2(p12CertificatePath, certificatePassword, X509KeyStorageFlags.Exportable);

			var credential = new ServiceAccountCredential(
			new ServiceAccountCredential.Initializer(serviceAccountEmail)
			{
				Scopes = new[] { AnalyticsService.Scope.Analytics }
			}.FromCertificate(certificate));

			service = new AnalyticsService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = applicationName,
			});

		}

		/// <summary>
		/// Simple method for getting the total number of unique visitors of a profile during a period
		/// </summary>
		/// <param name="profileId">The profile id in form of ga:xxxxxxxx</param>
		/// <param name="start">Start date</param>
		/// <param name="end">End date</param>
		/// <returns>Number of unique visitors</returns>
		public int GetVisitorsForProfile(string profileId, DateTime start, DateTime end)
		{

			var query = service.Data.Ga.Get(profileId, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"), "ga:visitors");
			
			Google.Apis.Analytics.v3.Data.GaData d = query.Execute();

			return int.Parse(d.Rows[0][0].ToString());
		}
	}
}
