using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AutenticacaoOwin.Controllers
{
    public class TesteController : ApiController
    {
        // GET: Teste
        [System.Web.Http.Authorize]
        public bool Get()
        {
            return true;
        }
        
    }
}