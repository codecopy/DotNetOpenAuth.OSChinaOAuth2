using System;
using System.Collections.Generic;
using System.Net;
using DotNetOpenAuth.Messaging;
using Newtonsoft.Json;

namespace DotNetOpenAuth.AspNet.Clients
{
    public class OSChinaClient : OAuth2Client
    {
        private const string AuthorizationEndpoint = "https://www.oschina.net/action/oauth2/authorize";
        private const string TokenEndpoint = "https://www.oschina.net/action/openapi/token";
        private const string UserEndpoint = "https://www.oschina.net/action/openapi/user";

        private readonly string _appId;
        private readonly string _appSecret;
        private readonly string _returnUrl;

        public OSChinaClient(string appId, string appSecret, string returnUrl = null) : base("oschina")
        {
            Requires.NotNullOrEmpty(appId, "appId");
            Requires.NotNullOrEmpty(appSecret, "appSecret");

            _appId = appId;
            _appSecret = appSecret;
            _returnUrl = (returnUrl == null) ? null : new Uri(returnUrl).AbsoluteUri.NormalizeHexEncoding();
        }

        protected override Uri GetServiceLoginUrl(Uri returnUrl)
        {
            var uriBuilder = new UriBuilder(AuthorizationEndpoint);
            var uri = _returnUrl ?? returnUrl.AbsoluteUri.NormalizeHexEncoding();
            uriBuilder.AppendQueryArgs(new Dictionary<string, string>
            {
                {
                    "client_id",
                    _appId
                },

                {
                    "redirect_uri",
                    uri
                },

                {
                    "response_type",
                    "code"
                }
            });
            return uriBuilder.Uri;
        }

        protected override string QueryAccessToken(Uri returnUrl, string authorizationCode)
        {
            var uriBuilder = new UriBuilder(TokenEndpoint);
            var uri = _returnUrl ?? returnUrl.AbsoluteUri.NormalizeHexEncoding(); 
            uriBuilder.AppendQueryArgs(new Dictionary<string, string>
            {
                {
                    "client_id",
                    _appId
                },

                {
                    "client_secret",
                    _appSecret
                },
                
                {
                    "grant_type",
                    "authorization_code"
                },

                {
                    "redirect_uri",
                    uri
                },
                
                {
                    "code",
                    authorizationCode
                },
            });
            string result;
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(uriBuilder.Uri);
                if (string.IsNullOrEmpty(json))
                {
                    result = null;
                }
                else
                {
                    var nameValueCollection = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    result = nameValueCollection["access_token"];
                }
            }
            return result;
        }

        protected override IDictionary<string, string> GetUserData(string accessToken)
        {
            var webRequest = WebRequest.Create(UserEndpoint + "?access_token=" + MessagingUtilities.EscapeUriDataStringRfc3986(accessToken));

            OSChinaGraphData facebookGraphData;
            using (var response = webRequest.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    facebookGraphData = JsonConvertX.Deserialize<OSChinaGraphData>(responseStream);
                }
            }
            
            var dictionary = new Dictionary<string, string>();
            dictionary.AddItemIfNotEmpty("id", facebookGraphData.Id);
            dictionary.AddItemIfNotEmpty("name", facebookGraphData.Name);
            dictionary.AddItemIfNotEmpty("email", facebookGraphData.Email);
            dictionary.AddItemIfNotEmpty("gender", facebookGraphData.Gender);
            dictionary.AddItemIfNotEmpty("avatar", (facebookGraphData.Avatar == null) ? null : facebookGraphData.Avatar.AbsoluteUri);
            dictionary.AddItemIfNotEmpty("url", (facebookGraphData.Link == null) ? null : facebookGraphData.Link.AbsoluteUri);
            dictionary.AddItemIfNotEmpty("location", facebookGraphData.Location);
            return dictionary;
        }
    }
}