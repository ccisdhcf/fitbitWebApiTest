using fitbitWebApiTest.tools;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fitbitWebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class FitbitController : ControllerBase
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
        public ActionResult Get()
        //public string Get()
        {
            strVerifier = _stringGen.RandomString(43, 128);
            Console.WriteLine(strVerifier);
            Console.WriteLine();
            strCodeChallenge = _stringGen.SHA256PlusBase64(strVerifier);
            Console.WriteLine(strCodeChallenge);
            Console.WriteLine();
            Console.WriteLine(string.Format(strAuthUrl, strClientID, strResponseType, strCodeChallenge, strCodeChallengeMethod, strScope));
            //Process.Start(new ProcessStartInfo("https://google.com") { UseShellExecute = true }); // Works ok on windows
            return Redirect(string.Format(strAuthUrl, strClientID, strResponseType, strCodeChallenge, strCodeChallengeMethod, strScope));
            //return string.Format(strAuthUrl, strClientID, strResponseType, strCodeChallenge, strCodeChallengeMethod, strScope);
        }
        [HttpPost]
        public ActionResult Post()
        {
            // fetch
            return Ok();
        }

    }
}
