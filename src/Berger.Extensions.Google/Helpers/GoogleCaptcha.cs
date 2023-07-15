using System.Net;
using Newtonsoft.Json.Linq;

namespace Berger.Extensions.Google
{
    public static class GoogleCaptcha
    {
        public static bool Check(string secret, string response)
        {
            var httpClient = new HttpClient();

            var uri = "https://www.google.com/recaptcha/api/siteverify";

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("response", response)});

            var result = httpClient.PostAsync(uri, data).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                string content = result.Content.ReadAsStringAsync().Result;

                dynamic confirmation = JObject.Parse(content);

                if (confirmation.success == true)
                    return true;
            }

            return false;
        }
    }
}