using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TuCartera.Models;

namespace TuCartera.Services
{
    public interface IFinancialApiService
    {
        #region Stocks

        Task<List<TickerCurrentValueDTO>> tickerCurrentValues(List<TickerDTO> tickers);

        #endregion
    }
    public class FinancialApiService : IFinancialApiService
    {
        #region Constants

        string BASE_PATH = "https://financialmodelingprep.com/api/v3/";
        string API_KEY = "";

        #endregion

        public FinancialApiService(string apiKey)
        {
            this.API_KEY = apiKey;
        }

        #region Stocks

        public async Task<List<TickerCurrentValueDTO>> tickerCurrentValues(List<TickerDTO> tickers)
        {
            List<TickerCurrentValueDTO> result = new List<TickerCurrentValueDTO>();
            var tickerCodes = String.Join(",", tickers.Select(ticker => ticker.Code));
            var requestUri = this.ApiRequestUri("profile", tickerCodes);

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(requestUri))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var apiresults = JsonConvert.DeserializeObject<List<TickerProfileResponse>>(apiResponse);

                    result = tickers.Join(apiresults, bdTicker => bdTicker.Code, apiTicker => apiTicker.Symbol,
                                            (bdTicker, apiTicker) => new TickerCurrentValueDTO(bdTicker.Id, apiTicker.Price))
                                      .ToList();
                }
            }
            return result;
        }

        #endregion

        #region Private methods

        private string ApiRequestUri(string ep, string tickerCodes, string queryParams = "")
        {
            var extraQuery = !string.IsNullOrEmpty(queryParams) ? $"&{queryParams}" : "";
            return $"{BASE_PATH}{ep}/{tickerCodes}?apikey={API_KEY}{extraQuery}";
        }

        #endregion
    }
}
