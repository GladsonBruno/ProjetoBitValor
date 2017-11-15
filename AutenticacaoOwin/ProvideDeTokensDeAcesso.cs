using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AutenticacaoOwin
{
    public class ProvideDeTokensDeAcesso: OAuthAuthorizationServerProvider
    {
        //Responsavel por fazer validações extras quando o usuario entra com o token de acesso
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
    }
}