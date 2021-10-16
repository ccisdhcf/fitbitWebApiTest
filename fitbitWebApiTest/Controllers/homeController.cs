using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fitbitWebApiTest.tools;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace fitbitWebApiTest.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private string strClientID = "23BKY4";
        private string strClientSecret = "4177e6ef04511a2e6d94406c12ba8ccd";
        private string strRedirectUrl = "https://google.com";
        private string strScope = "activity%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight";
        private string strCodeChallengeMethod = "S256";
        private string strResponseType = "code";
        private readonly stringGen _stringGen = new();
        private string strVerifier;
        private string strCodeChallenge;

        private string strAuthUrl = "https://www.fitbit.com/oauth2/authorize?client_id={0}&response_type={1}&code_challenge={2}&code_challenge_method={3}&scope={4}";

        [HttpGet]
        public  ActionResult Get()
        {
            strVerifier = _stringGen.RandomString(43, 128);
            strCodeChallenge = _stringGen.ComputeSha256Hash(strVerifier);
            strCodeChallenge = _stringGen.ComputeBase64Url(strCodeChallenge);
            Console.Write(string.Format(strAuthUrl, strClientID, strResponseType, strCodeChallenge, strCodeChallengeMethod, strScope));
            //Process.Start();
            Process.Start(new ProcessStartInfo("https://google.com") { UseShellExecute = true }); // Works ok on windows
            return Redirect(string.Format(strAuthUrl, strClientID, strResponseType, strCodeChallenge, strCodeChallengeMethod, strScope)); 
        }
    }

    
}
   
