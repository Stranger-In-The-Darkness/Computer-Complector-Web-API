using System.Collections.Generic;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.Models.Analytics.Requests;

namespace ComputerComplectorWebAPI.Interfaces
{
	/// <summary>
	/// Interface of analytics provider service
	/// </summary>
	public interface IAnalyticsServiceAsync
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

		/// <summary>
		/// Get properties
		/// </summary>
		/// <param name="componentType">Component type of properties</param>
		/// <returns><see cref="IEnumerable{T}"/> or properties names</returns>
		Task<IEnumerable<string>> GetProperties(string componentType);

		/// <summary>
		/// Get property values
		/// </summary>
		/// <param name="componentType">Component type of property</param>
		/// <param name="property">Property name</param>
		/// <returns><see cref="IEnumerable{T}"/> of values of property</returns>
		Task<IEnumerable<string>> GetPropertyValues(string componentType, string property);

		/// <summary>
		/// Add property
		/// </summary>
		/// <param name="componentType">Coponent type of property</param>
		/// <param name="name">Name of new property</param>
		/// <returns>Updated <see cref="IEnumerable{T}"/> of properties</returns>
		Task<IEnumerable<string>> AddProperty(string componentType, string name);

		/// <summary>
		/// Add property value
		/// </summary>
		/// <param name="componentType">Component type of property</param>
		/// <param name="property">Property name</param>
		/// <param name="value">Property value</param>
		/// <returns>Updated <see cref="IEnumerable{T}"/> of values of properties</returns>
		Task<IEnumerable<string>> AddPropertyValue(string componentType, string property, string value);
	}
}
