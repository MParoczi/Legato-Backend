using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Legato.Models;
using Legato.Models.UtilityModels;
using Newtonsoft.Json.Linq;

namespace Legato.Utilities
{
    /// <summary>
    ///     Contains methods for API calls
    /// </summary>
    public static class ApiCalls
    {
        /// <summary>
        ///     Fetch country names from restcountries.eu
        /// </summary>
        /// <returns>List of country names</returns>
        /// <exception cref="HttpRequestException">In case of failed api request</exception>
        public static async Task<List<string>> GetCountries()
        {
            var baseURL = "https://restcountries.eu/rest/v2/all";
            try
            {
                using var client = new HttpClient();
                using var res = await client.GetAsync(baseURL);
                using var content = res.Content;
                var data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    var jsonArray = JArray.Parse(data);
                    var countries = jsonArray.ToObject<List<Country>>();
                    return countries.Select(country => country.Name).ToList();
                }
            }
            catch (Exception)
            {
                throw new HttpRequestException();
            }

            return null;
        }
    }
}