using ProjetoBitValor.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjetoBitValor.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LoginController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string email, string senha)
        {
            try
            {
                Usuario user = new Usuario();

                return Ok(user.Recuperar(email, senha));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);                
            }
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
