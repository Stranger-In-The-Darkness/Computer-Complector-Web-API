using ComputerComplectorWebAPI.Models.Analytics;

namespace ComputerComplectorWebAPI.Helpers
{
	/// <summary>
	/// Application settings class
	/// </summary>
	public class AppSettings
	{
		/// <summary>
		/// Secret key for JWT token
		/// </summary>
		public string Secret { get; set; }

		public SelectionTimeSpan SelectionTimeSpan { get; set; }
	}
}