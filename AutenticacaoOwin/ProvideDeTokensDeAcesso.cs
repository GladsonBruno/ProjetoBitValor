using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AutenticacaoOwin
{
    public class ProvideDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        //Responsavel por fazer validações extras quando o usuario entra com o token de acesso
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuario = ValidaLogin.validarLogin(context.UserName, context.Password);

            if(usuario == false)
            {
                context.SetError("invalid_grant", "Usuario não encontrado ou senha incorreta");
                return;
            }

            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identidadeUsuario);
        }
    }
}