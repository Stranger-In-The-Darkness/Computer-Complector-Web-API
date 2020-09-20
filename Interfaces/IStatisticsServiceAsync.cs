using System.Collections.Generic;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.Models.Statistics.Requests;
using ComputerComplectorWebAPI.Models.Data.Special;

namespace ComputerComplectorWebAPI.Interfaces
{
	/// <summary>
	/// Interface of analytics provider service
	/// </summary>
	public interface IStatisticsServiceAsync
	{
		/// <summary>
		/// Get recomendation index for each <see cref="GetRecommendationRequest.ComponentType"/> record
		/// </summary>
		/// <param name="request"><see cref="GetRecommendationRequest"/> request</param>
		/// <param name="amount">Amount of records to return</param>
		/// <returns><see cref="IDictionary{int, int}"/> of intger ID's of elements and their recommendation level</returns>
		Task<IDictionary<int, int>> GetRecommended(GetRecommendationRequest request, int? amount = null);

		/// <summary>
		/// Add selected <see cref="AddSelectionRequest.ComponentType"/> to selected options
		/// </summary>
		/// <param name="request"><see cref="AddSelectionRequest"/> request</param>
		Task AddSelected(AddSelectionRequest request);
	}
}
