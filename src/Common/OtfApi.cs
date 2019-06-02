using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OtfTracker.Common.Models;
using OtfTracker.Common.Requests;
using OtfTracker.Common.Responses;

namespace OtfTracker.Common
{
    public class OtfApi
    {
        // private const string AWSRegion = "us-east-1";
        private const string CLIENT_ID = "3dt9jpd58ej69f4183rqjrsu7c";
        // private const string USERPOOL_ID = "us-east-1_dYDxUeyL1";
        // private const string IDENITYPOOL_ID = "us-east-1:4943c880-fb02-4fd7-bc37-2f4c32ecb2a3";
        // private const string IDENITY_PROVIDER = "cognito-idp.us-east-1.amazonaws.com/";
        private const string MANAGEMENT_API_BASE = "https://api.orangetheory.co/";
        private const string WORKOUT_DETAILS_API_BASE = "https://performance.orangetheory.co/";

        private readonly AmazonCognitoIdentityProviderClient _loginClient;

        public OtfApi()
        {
            _loginClient = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.USEast1);
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            LoginResponse loginResponse;
            InitiateAuthRequest auth = new InitiateAuthRequest()
            {
                ClientId = CLIENT_ID,
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
            };
            auth.AuthParameters.Add("USERNAME", username);
            auth.AuthParameters.Add("PASSWORD", password);
            InitiateAuthResponse response = await _loginClient.InitiateAuthAsync(auth);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = handler.ReadToken(response.AuthenticationResult.IdToken) as JwtSecurityToken;
                loginResponse = new LoginResponse()
                {
                    Email = jsonToken.Claims.Single(c => c.Type == "email").Value,
                    EmailVerified = bool.Parse(jsonToken.Claims.Single(c => c.Type == "email_verified").Value),
                    FamilyName = jsonToken.Claims.Single(c => c.Type == "family_name").Value,
                    GivenName = jsonToken.Claims.Single(c => c.Type == "given_name").Value,
                    HomeStudioId = jsonToken.Claims.Single(c => c.Type == "custom:home_studio_id").Value,
                    IsMigration = bool.Parse(jsonToken.Claims.Single(c => c.Type == "custom:isMigration").Value),
                    Locale = jsonToken.Claims.Single(c => c.Type == "locale").Value,
                    MemberId = jsonToken.Claims.Single(c => c.Type == "cognito:username").Value,
                    JwtToken = response.AuthenticationResult.IdToken,
                };
            }
            else
            {
                loginResponse = null;
            }

            return loginResponse;
        }

        private HttpClient GetDefaultHttpClient(string jwtToken)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("Accept", "application/json");
            http.DefaultRequestHeaders.Add("Authorization", jwtToken);
            return http;
        }

        private StringContent GetContentForHttp(object toSerialize)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(toSerialize));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        public async Task<IEnumerable<ClassSummary>> GetClassSummariesAsync(string memberId, string jwtToken)
        {
            ClassSummariesResponse response;
            using (HttpClient http = GetDefaultHttpClient(jwtToken))
            {
                string result = await http.GetStringAsync($"{MANAGEMENT_API_BASE}member/member/{memberId}/heart-rate");
                response = JsonConvert.DeserializeObject<ClassSummariesResponse>(result);
            }

            return response.Data;
        }

        public async Task<ClassDetails> GetClassDetailsAsync(string classId, string memberId, string jwtToken)
        {
            ClassDetails details;
            WorkoutDetailsRequest request = new WorkoutDetailsRequest() { ClassHistoryUuid = classId, MemberUuid = memberId };
            using (HttpClient http = GetDefaultHttpClient(jwtToken))
            using (StringContent content = GetContentForHttp(request))
            {
                HttpResponseMessage response = await http.PostAsync($"{WORKOUT_DETAILS_API_BASE}v2.4/member/workout/summary", content);
                string responseString = await response.Content.ReadAsStringAsync();
                details = JsonConvert.DeserializeObject<ClassDetails>(responseString);
            }

            return details;
        }

        public async Task<Dictionary<PersonalStatsTimeframes, PersonalStats>> GetPersonalStatsAsync(string memberId, string jwtToken)
        {
            Dictionary<PersonalStatsTimeframes, PersonalStats> stats = new Dictionary<PersonalStatsTimeframes, PersonalStats>();
            LifetimeStatsRequest request = new LifetimeStatsRequest() { AsOfDate = DateTime.Now, MemberUuid = memberId };
            using (HttpClient http = GetDefaultHttpClient(jwtToken))
            using (StringContent content = GetContentForHttp(request))
            {
                HttpResponseMessage response = await http.PostAsync($"{WORKOUT_DETAILS_API_BASE}v2.4/member/workout", content);
                string responseString = await response.Content.ReadAsStringAsync();
                LifetimeStatsResponse lifetimeResponse = JsonConvert.DeserializeObject<LifetimeStatsResponse>(responseString);
                stats[PersonalStatsTimeframes.AllTime] = lifetimeResponse.AllTime;
                stats[PersonalStatsTimeframes.LastMonth] = lifetimeResponse.LastMonth;
                stats[PersonalStatsTimeframes.LastWeek] = lifetimeResponse.LastWeek;
                stats[PersonalStatsTimeframes.LastYear] = lifetimeResponse.LastYear;
                stats[PersonalStatsTimeframes.ThisMonth] = lifetimeResponse.ThisMonth;
                stats[PersonalStatsTimeframes.ThisWeek] = lifetimeResponse.ThisWeek;
                stats[PersonalStatsTimeframes.ThisYear] = lifetimeResponse.ThisYear;
            }

            return stats;
        }
    }
}
