using fitbitWebApiTest.tools;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.IO;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fitbitWebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class FitbitController : ControllerBase
    {
        //private readonly string strClientSecret = "4177e6ef04511a2e6d94406c12ba8ccd";
        //private readonly string strRedirectUrl = "https://localhost:5001/api/Fitbit/callback";
        private string strClientID = "23BKY4";
        private readonly string strScope = "activity%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight";
        private readonly string strCodeChallengeMethod = "S256";
        private readonly string strResponseType = "code";
        private string strVerifier;
        private string strCodeChallenge;
        private readonly string strAuthUrl = "https://www.fitbit.com/oauth2/authorize?client_id={0}&response_type={1}&code_challenge={2}&code_challenge_method={3}&scope={4}";
        private readonly string strTokenUrl = "https://api.fitbit.com/oauth2/token";
        private readonly stringGen _stringGen = new();
        private static readonly HttpClient client = new HttpClient();
        string fileName = "verifier.json";

        //private readonly HttpContent content ;

        [HttpGet]
        public async Task<ActionResult> Get()
        //public string Get()
        {
            strVerifier = _stringGen.RandomString(43, 128);
            //Console.WriteLine(strVerifier);
            //json serialize
            string fileName = "verifier.json";
            using FileStream steam = System.IO.File.Create(fileName);
            await JsonSerializer.SerializeAsync(steam, strVerifier);
            await steam.DisposeAsync();
            
            strCodeChallenge = _stringGen.SHA256PlusBase64(strVerifier);
            //Console.WriteLine(strCodeChallenge);
            
            //Console.WriteLine(string.Format(strAuthUrl, strClientID, strResponseType, strCodeChallenge, strCodeChallengeMethod, strScope));
            Process.Start(new ProcessStartInfo(string.Format(strAuthUrl, strClientID, strResponseType, strCodeChallenge, strCodeChallengeMethod, strScope)) { UseShellExecute = true }); // Works ok on windows
            //return Redirect(string.Format(strAuthUrl, strClientID, strResponseType, strCodeChallenge, strCodeChallengeMethod, strScope));
            //return string.Format(strAuthUrl, strClientID, strResponseType, strCodeChallenge, strCodeChallengeMethod, strScope);
            return Ok();
        }
        [HttpGet]
        [Route("callback")]
        public async Task<ActionResult> GetCallBack([FromQuery] string code)
        {
            //json deserialize
            string jsonStr = System.IO.File.ReadAllText(fileName);
            strVerifier = JsonSerializer.Deserialize<string>(jsonStr);

       

            Dictionary<string, string> formDataDictionary = new Dictionary<string, string>();
            formDataDictionary.Add("Content-Type", "application/x-www-form-urlencoded");

            formDataDictionary.Add("client_id", "23BKY4");
            formDataDictionary.Add("code", code);
            formDataDictionary.Add("code_verifier", strVerifier);
            formDataDictionary.Add("grant_type", "authorization_code");
            foreach (KeyValuePair<string, string> i in formDataDictionary)
            {
                Console.WriteLine(i);
            }
            var formData = new FormUrlEncodedContent(formDataDictionary);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync(strTokenUrl, formData);
            Console.WriteLine("response");
            Console.WriteLine(response);
            if (response != null)
            {
                if (response.IsSuccessStatusCode == true)
                {
                    // 取得呼叫完成 API 後的回報內容
                    String strResult = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(strResult);
                    
                }
            }


                return Ok();
        }

    }
}
