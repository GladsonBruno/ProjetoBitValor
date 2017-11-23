using ProjetoBitValor.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjetoBitValor.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CadastrarController : ApiController
    {
        // GET: api/Cadastrar
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Cadastrar/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Cadastrar
        public IHttpActionResult Post(Usuario usuario)
        {
            try
            {
                Usuario user = new Usuario(usuario);

                user.Cadastrar();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);                
            }
        }

        // PUT: api/Cadastrar/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cadastrar/5
        public void Delete(int id)
        {
        }
    }
}
