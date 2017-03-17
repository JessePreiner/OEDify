using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OEDify
{
    public class OEDClient
    {
        private readonly ApiClient _client;
        public OEDClient()
        {
            _client = new ApiClient("caead899", "b3896eea56a41b6fc34d7c0c0c2a2a0d");
        }

        public async Task<ApiResponse> Do()
        {
            return await _client.Get("douche");

        }
    }

    public class ApiClient
    {
        private readonly string _app_id;
        private readonly string _app_key;
        private readonly IRestClient _client;

        public ApiClient(string app_id, string app_key)
        {
            _app_id = app_id;
            _app_key = app_key;
            _client = new RestClient("https://od-api.oxforddictionaries.com/api/v1");
        }

        public Task<ApiResponse> Get(string word)
        {
            var request = new RestRequest("entries/en/douche", Method.GET);
            request.AddHeader("app_id", "caead899");
            request.AddHeader("app_key", "b3896eea56a41b6fc34d7c0c0c2a2a0d");
            var tcs = new TaskCompletionSource<ApiResponse>();
            ApiResponse response;
            var meep = _client.ExecuteAsync(request, (a) =>
            {
                response = new ApiResponse(a);
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

    }


    #region Models
    public class Metadata
    {
        public string provider { get; set; }
    }

    public class Example
    {
        public string text { get; set; }
    }

    public class Example2
    {
        public string text { get; set; }
    }

    public class Subsens
    {
        public List<string> definitions { get; set; }
        public List<Example2> examples { get; set; }
        public string id { get; set; }
        public List<string> regions { get; set; }
    }

    public class Sens
    {
        public List<string> definitions { get; set; }
        public List<Example> examples { get; set; }
        public string id { get; set; }
        public List<Subsens> subsenses { get; set; }
    }

    public class VariantForm
    {
        public string text { get; set; }
    }

    public class GrammaticalFeature
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Entry
    {
        public List<string> etymologies { get; set; }
        public string homographNumber { get; set; }
        public List<Sens> senses { get; set; }
        public List<VariantForm> variantForms { get; set; }
        public List<GrammaticalFeature> grammaticalFeatures { get; set; }
    }

    public class Pronunciation
    {
        public string audioFile { get; set; }
        public List<string> dialects { get; set; }
        public string phoneticNotation { get; set; }
        public string phoneticSpelling { get; set; }
    }

    public class LexicalEntry
    {
        public List<Entry> entries { get; set; }
        public string language { get; set; }
        public string lexicalCategory { get; set; }
        public List<Pronunciation> pronunciations { get; set; }
        public string text { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string language { get; set; }
        public List<LexicalEntry> lexicalEntries { get; set; }
        public string type { get; set; }
        public string word { get; set; }
    }

    public class ApiResponse
    {
        public Metadata metadata { get; set; }
        public List<Result> results { get; set; }

        public ApiResponse()
        {

        }

        public ApiResponse(IRestResponse restResponse) {
            var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse>(restResponse.Content);
            metadata = deserialized.metadata;
            results = deserialized.results;
        }

        public ApiResponse(ApiResponse other)
        {
            metadata = other.metadata;
            results = other.results;
        }
    }
    #endregion
}


