using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fitbitWebApiTest.tools;

namespace fitbitWebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class homeController : ControllerBase
    {
        private string strClientID = "23BKY4";
        private string strClientSecret = "4177e6ef04511a2e6d94406c12ba8ccd";
        private string strRedirectUrl = "https://google.com";
        private readonly stringGen _stringGen = new();

        [HttpGet]
        public async Task< ActionResult> GetIndex()
        {
            string verifier = _stringGen.RandomString(43, 128);
            string challenge = _stringGen.ComputeSha256Hash(verifier);
            challenge = _stringGen.ComputeBase64Url(challenge);
            return null;
        }
    }

    
}
   
