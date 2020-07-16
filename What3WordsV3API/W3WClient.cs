using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Newtonsoft.Json;

using Curnow.Biz.What3WordsV3Net.Models;
using Curnow.Biz.What3WordsV3Net.Enums;

namespace Curnow.Biz.What3WordsV3Net
{
    public class W3WClient
    {
        private const string _apiURL = "https://api.what3words.com/v3/";
        private string _userAgent = $"what3words-Net/{Assembly.GetExecutingAssembly().GetName().Version.ToString()} (.NET {Environment.Version.ToString()}; {Environment.OSVersion.VersionString})";
        private readonly HttpClient _client;
        private string _apiKey;

        public string APIKey
        {
            get { return this._apiKey; }
            set { this._apiKey = value; }
        }

        public W3WClient(string apiKey)
        {
            this._apiKey = apiKey;
            _client = new HttpClient { BaseAddress = new Uri(_apiURL) };
            _client.DefaultRequestHeaders.Add("User-Agent", _userAgent);
        }

#region Public API instance methods

        /// <summary>
        /// Asynchronous method to convert a latitude and longitude to a 3 word address, in the language of your choice. It also returns country, the bounds of the grid square, a nearest place (such as a local town) and a link to the What3Words map site.
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lng">Longitude</param>
        /// <param name="lang">Language (optional. Defaults to en - english)</param>
        /// <returns>AddressResponse object</returns>
        public async Task<AddressResponse> ConvertTo3WAAsync(double lat, double lng, string lang="en")
        {
            return await GetDataAsync<AddressResponse>($"convert-to-3wa?coordinates={lat},{lng}&language={lang}&key={_apiKey}&format=json");
        }

        /// <summary>
        /// Syncronous method to convert a latitude and longitude to a 3 word address, in the language of your choice. It also returns country, the bounds of the grid square, a nearest place (such as a local town) and a link to the What3Words map site.
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lng">Longitude</param>
        /// <param name="lang">Language (optional. Defaults to en - english)</param>
        /// <returns></returns>
        public AddressResponse ConvertTo3WA(double lat, double lng, string lang="en")
        {
            return GetData<AddressResponse>($"convert-to-3wa?coordinates={lat},{lng}&language={lang}&key={_apiKey}&format=json");
        }

        /// <summary>
        /// Synchronous method to convert a 3 word address to a latitude and longitude. It also returns country, the bounds of the grid square, a nearest place (such as a local town) and a link to the What3Words map site.
        /// </summary>
        /// <param name="firstWord">First word</param>
        /// <param name="secondWord">Second word</param>
        /// <param name="thirdWord">Third word</param>
        /// <param name="lang">Language (defaults to en - english)</param>
        /// <returns>AddressResponse object</returns>
        public AddressResponse ConvertToCoordinates(string firstWord, string secondWord, string thirdWord, string lang="en")
        {
            return GetData<AddressResponse>($"convert-to-coordinates?words={firstWord}.{secondWord}.{thirdWord}&language={lang}&key={_apiKey}&format=json");
        }

        /// <summary>
        /// Asynchronous method to convert a 3 word address to a latitude and longitude. It also returns country, the bounds of the grid square, a nearest place (such as a local town) and a link to the What3Words map site.
        /// </summary>
        /// <param name="firstWord"></param>
        /// <param name="secondWord"></param>
        /// <param name="thirdWord"></param>
        /// <param name="lang"></param>
        /// <returns>AddressResponse object</returns>
        public async Task<AddressResponse> ConvertToCoordinatesAsync(string firstWord, string secondWord, string thirdWord, string lang = "en")
        {
            return await GetDataAsync<AddressResponse>($"convert-to-coordinates?words={firstWord}.{secondWord}.{thirdWord}&language={lang}&key={_apiKey}&format=json");
        }

        /// <summary>
        /// Syncronous method that retrieves a list all available 3 word address languages, including the ISO 639-1 2 letter code, english name and native name.
        /// </summary>
        /// <returns>LanguagesResponse object</returns>
        public LanguagesResponse AvailableLanguages()
        {
            return GetData<LanguagesResponse>($"available-languages?key={_apiKey}");
        }

        /// <summary>
        /// Asynchronous method that retrieves a list all available 3 word address languages, including the ISO 639-1 2 letter code, english name and native name.
        /// </summary>
        /// <returns>LanguagesResponse object</returns>
        public async Task<LanguagesResponse> AvailableLanguagesAsync()
        {
            return await GetDataAsync<LanguagesResponse>($"available-languages?key={_apiKey}");
        }

        /// <summary>
        /// Syncronous method that returns a section of the 3m x 3m what3words grid for a bounding box. The bounding box is specified by lat,lng,lat,lng as south,west,north,east.
        /// </summary>
        /// <param name="boundingBox">BoundingBox object containing north, east, south west latitude and longitude</param>
        /// <returns>GridSectionResponse object</returns>
        public GridSectionResponse GridSection(BoundingBox boundingBox)
        {
            return GetData<GridSectionResponse>($"grid-section?bounding-box={boundingBox.South},{boundingBox.West},{boundingBox.North},{boundingBox.East}&format=json&key={_apiKey}");
        }

        /// <summary>
        /// Asynchronous method that returns a section of the 3m x 3m what3words grid for a bounding box. The bounding box is specified by lat,lng,lat,lng as south,west,north,east.
        /// </summary>
        /// <param name="boundingBox">BoundingBox object containing north, east, south and west latitude and longitude</param>
        /// <returns></returns>
        public async Task<GridSectionResponse> GridSectionAsync(BoundingBox boundingBox)
        {
            return await GetDataAsync<GridSectionResponse>($"grid-section?bounding-box={boundingBox.South},{boundingBox.West},{boundingBox.North},{boundingBox.East}&format=json&key={_apiKey}");
        }

        /// <summary>
        /// Synchronouse method AutoSuggest can take a slightly incorrect 3 word address, and suggest a list of valid 3 word addresses. It has powerful features which can, for example, optionally limit results to a country or area, and prefer results which are near the user.
        /// </summary>
        /// <param name="input">The full or partial 3 word address to obtain suggestions for. At minimum this must be the first two complete words plus at least one character from the third word.</param>
        /// <param name="nResults">The number of AutoSuggest results to return. A maximum of 100 results can be specified, if a number greater than this is requested, this will be truncated to the maximum. The default is 3.</param>
        /// <param name="focus">This is a location, specified as latitude,longitude (often where the user making the query is). If specified, the results will be weighted to give preference to those near the focus. For convenience, longitude is allowed to wrap around the 180 line, so 361 is equivalent to 1.</param>
        /// <param name="nFocusResults">Specifies the number of results (must be less than or equal to n-results) within the results set which will have a focus. Defaults to n-results. This allows you to run autosuggest with a mix of focussed and unfocussed results, to give you a "blend" of the two. This is exactly what the old V2 standardblend did, and standardblend behaviour can easily be replicated by passing n-focus-results=1, which will return just one focussed result and the rest unfocussed.</param>
        /// <param name="clipToCountry">Restricts autosuggest to only return results inside the countries specified by comma-separated list of uppercase ISO 3166-1 alpha-2 country codes (for example, to restrict to Belgium and the UK, use clip-to-country=GB,BE). Clip-to-country will also accept lowercase country codes. Entries must be two a-z letters. WARNING: If the two-letter code does not correspond to a country, there is no error: API simply returns no results.</param>
        /// <param name="clipToBoundingBox">Restrict autosuggest results to a bounding box, specified by coordinates. Such as south_lat,west_lng,north_lat,east_lng, where: south_lat less than or equal to north_lat west_lng less than or equal to east_lng. In other words, latitudes and longitudes should be specified order of increasing size. Lng is allowed to wrap, so that you can specify bounding boxes which cross the ante-meridian: -4,178.2,22,195.4</param>
        /// <param name="clipToCircle">Restrict autosuggest results to a circle, specified by lat,lng,kilometres, where kilometres in the radius of the circle. For convenience, longitude is allowed to wrap around 180 degrees. For example 181 is equivalent to -179. </param>
        /// <param name="clipToPolygon">Restrict autosuggest results to a polygon, specified by a comma-separated list of lat, lng pairs.The polygon should be closed, i.e.the first element should be repeated as the last element; also the list should contain at least 4 entries.The API is currently limited to accepting up to 25 pairs. </param>
        /// <param name="inputType">For power users, used to specify voice input mode.Can be text(default), vocon-hybrid or nmdp-asr.See voice recognition section for more details.</param>
        /// <param name="language">Required for Voice. For normal text input, specifies a fallback language, which will help guide AutoSuggest if the input is particularly messy. If specified, this parameter must be a supported 3 word address language as an ISO 639-1 2 letter code. For voice input (see voice section), language must always be specified.</param>
        /// <param name="prefereLand">Makes autosuggest prefer results on land to those in the sea.This setting is on by default. Use false to disable this setting and receive more suggestions in the sea.</param>
        /// <returns>AutoSuggestResponse Object</returns>
        public AutoSuggestResponse AutoSuggest(string input, 
                                               int nResults=3, 
                                               Coordinates focus=null, 
                                               int nFocusResults=0, 
                                               string clipToCountry=null, 
                                               BoundingBox clipToBoundingBox=null,
                                               Circle clipToCircle=null,
                                               Polygon clipToPolygon=null,
                                               AutoSuggestInputType inputType=AutoSuggestInputType.Text,
                                               string language="en",
                                               bool prefereLand=true)
        {
            //build query string based on what has been set
            StringBuilder sb = new StringBuilder();
            if (focus != null)
                sb.Append($"&focus={focus.ToString()}");
            if (nFocusResults != 0)
                sb.Append($"&n-focus-results={nFocusResults}");
            if (clipToCountry != null)
                sb.Append($"clip-to-country={clipToCountry}");
            if (clipToBoundingBox != null)
                sb.Append($"&clip-to-bounding-box={clipToBoundingBox.ToString()}");
            if (clipToCircle != null)
                sb.Append($"&clip-to-circle={clipToCircle.ToString()}");
            if (clipToPolygon != null)
                sb.Append($"&clip-to-polygon={clipToPolygon.ToString()}");
            
            switch(inputType)
            {
                case AutoSuggestInputType.Text:
                    sb.Append($"&input-type=text");
                    break;
                case AutoSuggestInputType.NMDPASR:
                    sb.Append($"&input-type=nmdp-asr");
                    break;
                case AutoSuggestInputType.VoconHybrid:
                    sb.Append($"&input-type=vocon-hybrid");
                    break;
            }
            string queryString = $"autosuggest?input={input}&n-results={nResults}&language={language}&prefer-land={prefereLand.ToString().ToLower()}{sb.ToString()}&key={_apiKey}";

            return GetData<AutoSuggestResponse>(queryString);
        }

        /// <summary>
        /// Asynchronous method AutoSuggest can take a slightly incorrect 3 word address, and suggest a list of valid 3 word addresses. It has powerful features which can, for example, optionally limit results to a country or area, and prefer results which are near the user.
        /// </summary>
        /// <param name="input">The full or partial 3 word address to obtain suggestions for. At minimum this must be the first two complete words plus at least one character from the third word.</param>
        /// <param name="nResults">The number of AutoSuggest results to return. A maximum of 100 results can be specified, if a number greater than this is requested, this will be truncated to the maximum. The default is 3.</param>
        /// <param name="focus">This is a location, specified as latitude,longitude (often where the user making the query is). If specified, the results will be weighted to give preference to those near the focus. For convenience, longitude is allowed to wrap around the 180 line, so 361 is equivalent to 1.</param>
        /// <param name="nFocusResults">Specifies the number of results (must be less than or equal to n-results) within the results set which will have a focus. Defaults to n-results. This allows you to run autosuggest with a mix of focussed and unfocussed results, to give you a "blend" of the two. This is exactly what the old V2 standardblend did, and standardblend behaviour can easily be replicated by passing n-focus-results=1, which will return just one focussed result and the rest unfocussed.</param>
        /// <param name="clipToCountry">Restricts autosuggest to only return results inside the countries specified by comma-separated list of uppercase ISO 3166-1 alpha-2 country codes (for example, to restrict to Belgium and the UK, use clip-to-country=GB,BE). Clip-to-country will also accept lowercase country codes. Entries must be two a-z letters. WARNING: If the two-letter code does not correspond to a country, there is no error: API simply returns no results.</param>
        /// <param name="clipToBoundingBox">Restrict autosuggest results to a bounding box, specified by coordinates. Such as south_lat,west_lng,north_lat,east_lng, where: south_lat less than or equal to north_lat west_lng less than or equal to east_lng. In other words, latitudes and longitudes should be specified order of increasing size. Lng is allowed to wrap, so that you can specify bounding boxes which cross the ante-meridian: -4,178.2,22,195.4</param>
        /// <param name="clipToCircle">Restrict autosuggest results to a circle, specified by lat,lng,kilometres, where kilometres in the radius of the circle. For convenience, longitude is allowed to wrap around 180 degrees. For example 181 is equivalent to -179. </param>
        /// <param name="clipToPolygon">Restrict autosuggest results to a polygon, specified by a comma-separated list of lat, lng pairs.The polygon should be closed, i.e.the first element should be repeated as the last element; also the list should contain at least 4 entries.The API is currently limited to accepting up to 25 pairs. </param>
        /// <param name="inputType">For power users, used to specify voice input mode.Can be text(default), vocon-hybrid or nmdp-asr.See voice recognition section for more details.</param>
        /// <param name="language">Required for Voice. For normal text input, specifies a fallback language, which will help guide AutoSuggest if the input is particularly messy. If specified, this parameter must be a supported 3 word address language as an ISO 639-1 2 letter code. For voice input (see voice section), language must always be specified.</param>
        /// <param name="prefereLand">Makes autosuggest prefer results on land to those in the sea.This setting is on by default. Use false to disable this setting and receive more suggestions in the sea.</param>
        /// <returns>AutoSuggestResponse Object</returns>
        public async Task<AutoSuggestResponse> AutoSuggestAsync(string input,
                                                                int nResults = 3,
                                                                Coordinates focus = null,
                                                                int nFocusResults = 0,
                                                                string clipToCountry = null,
                                                                BoundingBox clipToBoundingBox = null,
                                                                Circle clipToCircle = null,
                                                                Polygon clipToPolygon = null,
                                                                AutoSuggestInputType inputType = AutoSuggestInputType.Text,
                                                                string language = "en",
                                                                bool prefereLand = true)
        {
            //build query string based on what has been set
            StringBuilder sb = new StringBuilder();
            if (focus != null)
                sb.Append($"&focus={focus.ToString()}");
            if (nFocusResults != 0)
                sb.Append($"&n-focus-results={nFocusResults}");
            if (clipToCountry != null)
                sb.Append($"clip-to-country={clipToCountry}");
            if (clipToBoundingBox != null)
                sb.Append($"&clip-to-bounding-box={clipToBoundingBox.ToString()}");
            if (clipToCircle != null)
                sb.Append($"&clip-to-circle={clipToCircle.ToString()}");
            if (clipToPolygon != null)
                sb.Append($"&clip-to-polygon={clipToPolygon.ToString()}");

            switch (inputType)
            {
                case AutoSuggestInputType.Text:
                    sb.Append($"&input-type=text");
                    break;
                case AutoSuggestInputType.NMDPASR:
                    sb.Append($"&input-type=nmdp-asr");
                    break;
                case AutoSuggestInputType.VoconHybrid:
                    sb.Append($"&input-type=vocon-hybrid");
                    break;
            }
            string queryString = $"autosuggest?input={input}&n-results={nResults}&language={language}&prefer-land={prefereLand.ToString().ToLower()}{sb.ToString()}&key={_apiKey}";

            return await GetDataAsync<AutoSuggestResponse>(queryString);
        }

#endregion

#region Private methods

        /// <summary>
        /// Generic async method to make HTTP call and return object of type TResponse
        /// </summary>
        /// <typeparam name="TResponse">Response type to return</typeparam>
        /// <param name="queryString">API end-point Query String</param>
        /// <returns></returns>
        private async Task<TResponse> GetDataAsync<TResponse>(string queryString) where TResponse : class
        {
            using (HttpResponseMessage response = await _client.GetAsync(queryString))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var err = await response.Content.ReadAsStringAsync();
                    HandleError(JsonConvert.DeserializeObject<W3WError>(err), response.StatusCode);
                }
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(json);
            }
        }

        /// <summary>
        /// Generic synchronous method to make HTTP call and return object of type TResponse
        /// </summary>
        /// <typeparam name="TResponse">Response type to return</typeparam>
        /// <param name="queryString">API end-point Query String</param>
        /// <returns></returns>
        private TResponse GetData<TResponse>(string queryString) where TResponse : class
        {
            using (HttpResponseMessage response = Task.Run(() => _client.GetAsync(queryString)).Result)
            {
                if (!response.IsSuccessStatusCode)
                {
                    var err = JsonConvert.DeserializeObject<W3WError>(response.Content.ReadAsStringAsync().Result);
                    HandleError(err, response.StatusCode);
                }
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result);
            }
        }

        /// <summary>
        /// Exception handling method. Throws a W3WException with information returned from W3W API
        /// </summary>
        /// <param name="err">Returned W3WError object</param>
        /// <param name="statusCode">HTTP Response Status Code</param>
        private void HandleError(W3WError err, HttpStatusCode statusCode)
        {
            switch(statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new W3WException($"401 - {err.error.code}", err.error.message);
                case HttpStatusCode.BadRequest:
                    throw new W3WException($"400 - {err.error.code}", err.error.message);
                case HttpStatusCode.NotFound:
                    throw new W3WException("404 - Not Found", "Check the What3Words API End-point URL");
                default:
                    throw new W3WException($"{statusCode.ToString()}", err.error.message);
            }
        }

#endregion

    }
}
